using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext _context, ILogger<DiscountService> _logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Models.Coupon { ProductName = "No Discount", Amount = 0, Code = "null", Description = "no description", Id = 0, IsActive = false };

            _logger.LogInformation($"Discount is retrieved for product name {coupon.ProductName} , amount : {coupon.Amount} ");

            return coupon.Adapt<CouponModel>();

        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request?.Coupon == null)
            {
                _logger.LogWarning("CreateDiscount called with null Coupon.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon must not be null."));
            }

            var couponEntity = request.Coupon.Adapt<Models.Coupon>();

            // Check for duplicate product name
            var exists = await _context.Coupons.AnyAsync(x => x.ProductName == couponEntity.ProductName);
            if (exists)
            {
                _logger.LogWarning("Coupon for product {ProductName} already exists.", couponEntity.ProductName);
                throw new RpcException(new Status(StatusCode.AlreadyExists, $"Coupon for product {couponEntity.ProductName} already exists."));
            }

            await _context.Coupons.AddAsync(couponEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon created for product {ProductName} with amount {Amount}.", couponEntity.ProductName, couponEntity.Amount);

            return couponEntity.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request?.Coupon == null)
            {
                _logger.LogWarning("UpdateDiscount called with null Coupon.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon must not be null."));
            }

            var couponModel = request.Coupon;
            var couponEntity = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == couponModel.Id);

            if (couponEntity == null)
            {
                _logger.LogWarning("Coupon with Id {Id} not found for update.", couponModel.Id);
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with Id {couponModel.Id} not found."));
            }

            // Update fields
            couponEntity.ProductName = couponModel.ProductName;
            couponEntity.Description = couponModel.Description;
            couponEntity.Amount = couponModel.Amount;
            couponEntity.Code = couponModel.Code;
            couponEntity.IsActive = couponModel.IsActive;

            _context.Coupons.Update(couponEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon with Id {Id} updated for product {ProductName}.", couponEntity.Id, couponEntity.ProductName);

            return couponEntity.Adapt<CouponModel>();
        }


        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            if (string.IsNullOrWhiteSpace(request?.ProductName))
            {
                _logger.LogWarning("DeleteDiscount called with null or empty ProductName.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "ProductName must not be null or empty."));
            }

            var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
            {
                _logger.LogWarning("Coupon for product {ProductName} not found for deletion.", request.ProductName);
                return new DeleteDiscountResponse { Success = false };
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon for product {ProductName} deleted.", request.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }

    }
}
