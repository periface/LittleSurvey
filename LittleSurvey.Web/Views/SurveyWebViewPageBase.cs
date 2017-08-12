using Abp.Web.Mvc.Views;

namespace LittleSurvey.Web.Views
{
    public abstract class LittleSurveyWebViewPageBase : LittleSurveyWebViewPageBase<dynamic>
    {

    }

    public abstract class LittleSurveyWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected LittleSurveyWebViewPageBase()
        {
            LocalizationSourceName = LittleSurveyConsts.LocalizationSourceName;
        }
    }
}