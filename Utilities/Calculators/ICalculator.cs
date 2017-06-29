namespace Utilities.Calculators
{
    public interface ICalculator<T, R>
    {
        R Calculate(T input);
    }
}