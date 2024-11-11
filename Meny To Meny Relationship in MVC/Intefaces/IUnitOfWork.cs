namespace Meny_To_Meny_Relationship_in_MVC.Intefaces
{
    public interface IUnitOfWork
    {
        IPost post { get; }

        ITag tag { get; }

        
    }
}
