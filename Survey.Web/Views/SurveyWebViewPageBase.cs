using Abp.Web.Mvc.Views;

namespace Survey.Web.Views
{
    public abstract class SurveyWebViewPageBase : SurveyWebViewPageBase<dynamic>
    {

    }

    public abstract class SurveyWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SurveyWebViewPageBase()
        {
            
        }
    }
}