using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Telerik.Windows.Controls.Charting;

namespace Telerik.Windows.Examples
{
    public class UserDataPoint
    {
        public double Y { get; set; }
        public double X { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double BubbleSize { get; set; }
        public string LegendLabel { get; set; }
    }

    public static class SeriesExtensions
    {
        private static double[,] constsY = new double[3, 12] { {24, 12, 18, 31, 25, 13, 17, 33, 21, 28, 19, 11},
                                                              {6, 19, 28, 11, 15, 31, 27, 14, 19, 21, 30, 15},
                                                              {17, 8, 31, 22, 26, 8, 23, 17, 11, 19, 24, 29}};
        //private static double[,] constsY = new double[3, 12] { {24, 9, 18, 31, 25, 13, 17, 33, 21, 28, 19, 11},
        //                                                      {6, 19, 28, 11, 15, 31, 27, 14, 19, 21, 30, 15},
        //                                                      {17, 8, 31, 22, 26, 12, 23, 17, 28, 19, 24, 29}};
        private static double[,] constsRange = new double[2, 12] { { 24, 9, 18, 31, 25, 13, 17, 33, 21, 28, 19, 11 },
                                                                   { 17, 6, 12, 22, 20, 8, 13, 28, 18, 19, 18, 7}};
        private static double[,] constsFinancial = new double[4, 7] { {24, 29, 31, 28, 25, 22, 26},
                                                                      {16, 18, 24, 16, 15, 18, 16},
                                                                      {17, 19, 25, 27, 17, 19, 19},
                                                                      {19, 25, 27, 17, 19, 19, 17}};

        public static double[] GetUserData(int numberOfItems, int seriesIndex)
        {
            int num = numberOfItems % 12 == 0 ? 12 : numberOfItems % 12;
            int ind = seriesIndex % 3;
            double[] points = new double[num];

            for (int i = 0; i < num; i++)
                points[i] = constsY[ind, i];

            return points;
        }

        public static List<UserDataPoint> GetUserBubbleData()
        {
            List<UserDataPoint> points = new List<UserDataPoint>();

            points.Add(CreateBubbleUserDataPoint(20, -40));
            points.Add(CreateBubbleUserDataPoint(40, 80));
            points.Add(CreateBubbleUserDataPoint(80, -20));
            points.Add(CreateBubbleUserDataPoint(60, 100));
            points.Add(CreateBubbleUserDataPoint(10, 20));

            return points;
        }

        public static List<UserDataPoint> GetUserBubbleMixedData()
        {
            List<UserDataPoint> points = new List<UserDataPoint>();

            points.Add(CreateBubbleUserDataPoint(75, -60));
            points.Add(CreateBubbleUserDataPoint(10, 40));
            points.Add(CreateBubbleUserDataPoint(40, -60));
            points.Add(CreateBubbleUserDataPoint(20, 50));
            points.Add(CreateBubbleUserDataPoint(50, 80));
            points.Add(CreateBubbleUserDataPoint(30, -40));

            return points;
        }

        private static UserDataPoint CreateBubbleUserDataPoint(double valueY, double bubbleSize)
        {
            UserDataPoint dataPnt = new UserDataPoint();
            dataPnt.Y = valueY;
            dataPnt.BubbleSize = bubbleSize;
            return dataPnt;
        }

        public static List<UserDataPoint> GetUserFinancialData(int numberOfItems)
        {
            List<UserDataPoint> points = new List<UserDataPoint>();
            int num = numberOfItems % 7 == 0 ? 7 : numberOfItems % 7;

            for (int i = 0; i < numberOfItems; i++)
            {
                UserDataPoint point = new UserDataPoint();
                point.Open = constsFinancial[2, i % num];
                point.Close = constsFinancial[3, i % num]; ;
                point.Low = constsFinancial[1, i % num]; ;
                point.High = constsFinancial[0, i % num]; ;

                points.Add(point);
            }

            return points;
        }

        public static double[] GetUserRadialData()
        {
            return GetUserData(7, 0);
        }

        public static List<UserDataPoint> GetUserRangeData(int numberOfItems)
        {
            List<UserDataPoint> points = new List<UserDataPoint>();
            int num = numberOfItems % 12 == 0 ? 12 : numberOfItems % 12;

            for (int i = 1; i < num; i++)
            {
                UserDataPoint data = new UserDataPoint();

                data.Low = constsRange[1, i];
                data.High = constsRange[0, i];
                points.Add(data);
            }

            return points;
        }


        public static void FillWithSampleData(DataSeries series)
        {
            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));
			FillWithSampleData(series, random.Next(10, 15));
        }

        public static void FillWithSampleData(DataSeries series, int numberOfItems, int max, int deviation)
        {
            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));

            if (deviation > max)
                deviation = max;

            for (int i = 0; i < numberOfItems; i++)
            {
                series.Add(new DataPoint(random.Next(max - deviation, max)));
            }
        }

        public static void FillWithSampleData(DataSeries series, int numberOfItems, int sum)
        {
            int localSum = 0;

            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));

            for (int i = 0; i < numberOfItems; i++)
            {
                int randomNumber = 0;

                while (randomNumber <= 0)
                    randomNumber = random.Next(sum / numberOfItems - 3, sum / numberOfItems + 3);

                if (localSum + randomNumber > sum)
                    randomNumber = sum - localSum;

                if ((i == numberOfItems - 1) && (localSum + randomNumber < sum))
                    randomNumber = sum - localSum;

                localSum += randomNumber;

                DataPoint dataPoint = new DataPoint();
                dataPoint.YValue = randomNumber;

                series.Add(dataPoint);
            }           
        }

        public static void FillWithSampleRadialData(DataSeries series)
        {
            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));
			FillWithSampleData(series, random.Next(4, 7));
        }

        public static void FillWithSampleBubbleData(DataSeries series)
        {
            series.Add(new DataPoint { YValue = 20, BubbleSize = 40 });
            series.Add(new DataPoint { YValue = 40, BubbleSize = 80 });
            series.Add(new DataPoint { YValue = 80, BubbleSize = 20 });
            series.Add(new DataPoint { YValue = 60, BubbleSize = 100 });
            series.Add(new DataPoint { YValue = 10, BubbleSize = 20 });
        }

        public static void FillWithSampleBubbleMixedData(DataSeries series)
        {
            series.Add(new DataPoint { YValue = 75, BubbleSize = -60 });
            series.Add(new DataPoint { YValue = 10, BubbleSize = 40 });
            series.Add(new DataPoint { YValue = 40, BubbleSize = -60 });
            series.Add(new DataPoint { YValue = 20, BubbleSize = 50 });
            series.Add(new DataPoint { YValue = 50, BubbleSize = 80 });
            series.Add(new DataPoint { YValue = 30, BubbleSize = -40 });
        }

        public static void FillWithSampleData(DataSeries series, int numberOfItems)
        {
            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));
            for (int i = 0; i < numberOfItems; i++)
            {
                int randomNumber = random.Next(30, 100);
                series.Add(new DataPoint { YValue = randomNumber });
            }
        }

        public static void FillWithSampleFinancialData(DataSeries series, int numberOfItems, int numberOfPeaks)
        {
            Random random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));

            int[] openArray = new int[numberOfItems];
            int[] closeArray = new int[numberOfItems];
            int[] maxArray = new int[numberOfItems];
            int[] minArray = new int[numberOfItems];

            int[] peakIndices = new int[numberOfPeaks];

            // Calculate peak indices
            for (int i = 1; i <= numberOfPeaks; i++)
            {
                peakIndices[i - 1] = Math.Min(i * numberOfItems / numberOfPeaks, numberOfItems - 1);
            }

            bool trend = false;

            int currentIndex = 0;
            int startValue = 20;

            //Calculate open close values
            for (int j = 0; j < numberOfPeaks; j++)
            {
                trend = !trend;

                int peakIndex = numberOfItems - 1;

                try
                {
                    peakIndex = peakIndices[j];
                }
                catch { }

                for (int k = currentIndex + 1; k <= peakIndex; k++, currentIndex++)
                {
                    int factor = random.Next(1, 5) ;

                    if (trend)
                    {
                        openArray[k] = random.Next(Math.Abs(startValue + factor), Math.Abs(startValue + (int)1.1 * factor));
                    }
                    else
                    {
                        int min = Math.Min(Math.Abs(startValue - factor), Math.Abs(startValue - (int)1.1 * factor));
                        int max = Math.Max(Math.Abs(startValue - (int)1.1 * factor), Math.Abs(startValue - factor));

                        openArray[k] = random.Next(min, max);
                    }
                    closeArray[k - 1] = openArray[k];
                }
            }

            openArray[0] = random.Next(startValue, closeArray[0]);
            closeArray[numberOfItems - 1] = random.Next(openArray[numberOfItems - 1], openArray[numberOfItems - 1] + 15);

            random = new Random((int)(series.GetHashCode() + DateTime.Now.Ticks));

            //Calculate high values
            for (int i = 0; i < numberOfItems; i++)
            {
                int max = Math.Max(openArray[i], closeArray[i]);
                int abs = Math.Abs(openArray[i] - closeArray[i]);

                do
                {
                    maxArray[i] = random.Next(max + 1, max + 3 );
                }
                while (maxArray[i] <= max);
            }

            //Calculate low values
            for (int i = 0; i < numberOfItems; i++)
            {
                int min = Math.Min(openArray[i], closeArray[i]);

                Debug.Assert(min >= 0, "Open or close value is negative");

                int abs = Math.Abs(openArray[i] - closeArray[i]);

                do
                {
                    if (min > 0)
                        minArray[i] = Math.Abs(random.Next(min - 3 , min - 1));
                    else
                        minArray[i] = 0;
                }
                while ( minArray[i] > min || minArray[i] < 0);
            }

            List<DataPoint> randomPoints = new List<DataPoint>();

            for (int i = 0; i < numberOfItems; i++)
            {
                DataPoint point = new DataPoint();
                point.Open = openArray[i];
                point.Close = closeArray[i];
                point.Low = minArray[i];
                point.High = maxArray[i];

                randomPoints.Add(point);
            }

            IEnumerable<DataPoint> invalid = from r in randomPoints
                                             where r.Low < 0 || r.Open < 0 || r.High < 0 || r.Close < 0
                                             select r;

            Debug.Assert(invalid.Count() == 0, "The generated data contains negative values.");

            series.AddRange(randomPoints);
        }

		public static void FillWithSampleRangeData(DataSeries series, int numberOfItems)
		{
			Random r = new Random();
			DataPoint data = new DataPoint();
			data.Low = r.Next(20, 70);
			data.High = data.Low + r.Next(20, 30);
			series.Add(data);
			for (int i = 1; i < numberOfItems; i++)
			{
				data = new DataPoint();
				int change = r.Next(0, 24) - 12;
				data.Low = series[i - 1].Low + change;
				data.High = Math.Max(series[i - 1].High + change + r.Next(0, 12) - 6, data.Low + 4);
				series.Add(data);
			}
		}
    }
}