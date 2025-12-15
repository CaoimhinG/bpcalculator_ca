using BPCalculator;
using Xunit;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        // Low Blood Pressure tests
        [Theory]
        [InlineData(85, 70)]   // Low systolic
        [InlineData(120, 55)]  // Low diastolic
        [InlineData(80, 50)]   // Both low
        public void Category_ReturnsLow_WhenBelowThresholds(int systolic, int diastolic)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(BPCategory.Low, bp.Category);
        }

        // Ideal Blood Pressure tests
        [Theory]
        [InlineData(90, 60)]   // Lower boundary
        [InlineData(110, 70)]  // Mid-range ideal
        [InlineData(119, 79)]  // Upper boundary
        public void Category_ReturnsIdeal_WhenInIdealRange(int systolic, int diastolic)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(BPCategory.Ideal, bp.Category);
        }

        // Pre-High Blood Pressure tests
        [Theory]
        [InlineData(100, 20)]  // Systolic at pre-high threshold [InlineData(120, 70)] 
        [InlineData(139, 75)]  // Systolic at upper pre-high
        [InlineData(110, 80)]  // Diastolic at pre-high threshold
        [InlineData(115, 89)]  // Diastolic at upper pre-high
        [InlineData(130, 85)]  // Both in pre-high range
        public void Category_ReturnsPreHigh_WhenSlightlyElevated(int systolic, int diastolic)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(BPCategory.PreHigh, bp.Category);
        }

        // High Blood Pressure tests
        [Theory]
        [InlineData(140, 70)]  // High systolic only
        [InlineData(110, 90)]  // High diastolic only
        [InlineData(150, 95)]  // Both high
        [InlineData(180, 100)] // Very high
        public void Category_ReturnsHigh_WhenAboveThresholds(int systolic, int diastolic)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            Assert.Equal(BPCategory.High, bp.Category);
        }

        // Boundary tests
        [Fact]
        public void Category_AtExactBoundary_SystolicAt90DiastolicAt60_ReturnsIdeal()
        {
            var bp = new BloodPressure { Systolic = 90, Diastolic = 60 };
            Assert.Equal(BPCategory.Ideal, bp.Category);
        }

        [Fact]
        public void Category_AtExactBoundary_SystolicAt89_ReturnsLow()
        {
            var bp = new BloodPressure { Systolic = 89, Diastolic = 70 };
            Assert.Equal(BPCategory.Low, bp.Category);
        }
    }
}