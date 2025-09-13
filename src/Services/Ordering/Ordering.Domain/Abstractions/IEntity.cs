
namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }


    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string? LastModifiedBy { get; set; }

    }
}
