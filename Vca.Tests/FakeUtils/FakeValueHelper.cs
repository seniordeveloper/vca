using Microsoft.AspNetCore.Http;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vca.Tests.FakeUtils
{
    [ExcludeFromCodeCoverage]
    public static class FakeValueHelper
    {
        private static readonly DateTime StartDate = new DateTime(1995, 1, 1).Date;

        private static int _lastIntValue;

        public static readonly Random Random = new();

        /// <summary>
        /// Returns <see cref="int"/> value which has not yet been repeated.
        /// </summary>
        public static int NextInt
        {
            get
            {
                try
                {
                    _lastIntValue = checked(_lastIntValue + 1);
                    return _lastIntValue;
                }
                catch (OverflowException)
                {
                    throw new Exception("Next int value is not unique.");
                }
            }
        }

        /// <summary>
        /// Returns <see cref="string"/> which has not yet been repeated.
        /// </summary>
        public static string NextString => Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        /// <summary>
        /// Returns random <see cref="bool"/> value.
        /// </summary>
        public static bool RandomBool => Random.Next(0, 2) > 0;

        /// <summary>
        /// Returns an <see cref="int"/> with a random value across the entire range of possible values.
        /// </summary>
        public static int RandomInt => Random.Next(0, 1 << 4) << 28 | Random.Next(0, 1 << 28);

        /// <summary>
        /// Returns an <see cref="decimal"/> with a random value across the entire range of possible values.
        /// </summary>
        public static decimal RandomDecimal =>
            new(RandomInt, RandomInt, RandomInt, RandomBool, (byte)Random.Next(29));

        public static double RandomDouble => Random.NextDouble();

        /// <summary>
        /// Returns random <see cref="DateTime"/> at range from 01.01.1995 to now.
        /// </summary>
        public static DateTime RandomDate => StartDate.AddDays(Random.Next((DateTime.Today - StartDate).Days)).Date;

        /// <summary>
        /// Returns today with random time.
        /// </summary>
        public static DateTime RandomTime => StartDate.AddDays(Random.Next((int)new TimeSpan(1, 0, 0, 0).TotalSeconds));

        /// <summary>
        /// Returns random phone number in format +000000000000
        /// </summary>
        public static string RandomPhoneNumber
        {
            get
            {
                var sBuilder = new StringBuilder();
                sBuilder.Append('+');
                for (var i = 0; i < 12; i++)
                {
                    sBuilder.Append(Random.Next(0, 9));
                }

                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Returns random value of <see cref="T"/> enum.
        /// </summary>
        public static T GetRandomEnum<T>()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Input type is not Enum.");
            }

            var values = Enum.GetValues(enumType);
            var index = Random.Next(values.Length);
            return (T)values.GetValue(index);
        }

        /// <summary>
        /// Returns non-existent <see cref="T"/> enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GetIncorrectEnumValue<T>()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Input type is not Enum.");
            }

            var last = Enum.GetValues(enumType).Cast<int>().Last();
            return last + 1;
        }

        public static IFormFile GetFileMock(string pathToFile) => AsMockIFormFile(new FileInfo(pathToFile));

        private static IFormFile AsMockIFormFile(FileInfo physicalFile)
        {
            var fileMock = new Mock<IFormFile>();

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;

            var fileName = physicalFile.Name;

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            fileMock.Setup(m => m.ContentDisposition).Returns($"inline; filename={fileName}");

            return fileMock.Object;
        }
    }
}
