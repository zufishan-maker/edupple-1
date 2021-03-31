namespace EDUPPLE.DOMAIN.Interface
{
    public interface IHaveIdentifier<TKey>
    {
        public TKey Id { get; set; }
    }
}
