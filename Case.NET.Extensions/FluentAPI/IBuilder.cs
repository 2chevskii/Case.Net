namespace Case.NET.Extensions.FluentAPI
{
    public interface IBuilder<out TSubject>
    {
        TSubject Build();
    }
}
