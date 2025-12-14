using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BPCalculator.Pages
{
    public class BloodPressureModel : PageModel
    {
        [BindProperty]
        public BloodPressure BP { get; set; }

        public string RecommendationMessage { get; set; }
        public string CssColor { get; set; }

        public void OnGet()
        {
            // Do not set default values — user must enter them
            BP = new BloodPressure();
        }

        public IActionResult OnPost()
        {
            if (BP == null)
            {
                ModelState.AddModelError("", "Please enter systolic and diastolic values.");
            }
            else if (!(BP.Systolic > BP.Diastolic))
            {
                ModelState.AddModelError("", "Systolic must be greater than Diastolic");
            }

            if (ModelState.IsValid)
            {
                (RecommendationMessage, CssColor) = BP.Category switch
                {
                    BPCategory.Low => (
                        "Your blood pressure is low. Consider consulting a healthcare provider if you feel dizzy or fatigued.",
                        "#cce5ff" // light blue
                    ),
                    BPCategory.Ideal => (
                        "Your blood pressure is ideal. Maintain a healthy lifestyle to keep it that way.",
                        "#d4edda" // green
                    ),
                    BPCategory.PreHigh => (
                        "Your blood pressure is slightly elevated. Monitor regularly and consider lifestyle changes.",
                        "#fff3cd" // yellow
                    ),
                    BPCategory.High => (
                        "Your blood pressure is high. Please seek medical advice as soon as possible.",
                        "#f8d7da" // red
                    ),
                    _ => ("", "#e2e3e5")
                };
            }

            return Page();
        }
    }
}
