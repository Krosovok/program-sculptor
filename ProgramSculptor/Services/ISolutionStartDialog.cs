using Model;

namespace Services
{
    public interface ISolutionStartDialog
    {
        Task Task { get; }

        Solution ShowDialog();
    }
}