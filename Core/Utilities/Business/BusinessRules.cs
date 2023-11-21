using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult CheckLogic(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                    return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
