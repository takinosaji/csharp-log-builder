using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace LogBuilder.Tests
{
    public class BatchedLogsBuilderTests
    {
        public class CreateBatches
        {
            [Theory, AutoData]
            public void ShouldReturnCorrectAmountOfLogs_WhenSeveralBatchableProperties(
                Fixture fixture)
            {
                //Arrange
                fixture.RepeatCount = 120;

                var firstBatchableProperty = new LogProperty(
                    fixture.Create<string>(),
                    fixture.Create<IEnumerable<string>>(),
                    7);
                var secondBatchableProperty = new LogProperty(
                    fixture.Create<string>(),
                    fixture.Create<IEnumerable<string>>(),
                    10);
                var thirdBatchableProperty = new LogProperty(
                    fixture.Create<string>(),
                    fixture.Create<IEnumerable<string>>(),
                    23);
                var firstCommonProperty = new LogProperty(fixture.Create<string>(), fixture.Create<string>());
                var secondCommonProperty = new LogProperty(fixture.Create<string>(), fixture.Create<string>());

                var properties = new[]
                {
                    firstCommonProperty, secondCommonProperty,
                    firstBatchableProperty, secondBatchableProperty,
                    thirdBatchableProperty
                };

                //Act
                var batchedLogProperties = BatchedLogPropertiesFactory.CreateBatches(properties);

                //Assert
                batchedLogProperties.Should().HaveCount(18);
            }

            [Theory, AutoData]
            public void ShouldReturnBatchesWithCorrectProperties_WhenBachable(
                Fixture fixture)
            {
                //Arrange
                const int batchSize = 10;

                fixture.RepeatCount = 20;
                var batchableValue = fixture.Create<IEnumerable<string>>();
                var batchableProperty = new LogProperty(
                    fixture.Create<string>(),
                    batchableValue,
                    batchSize);
                var commonProperty = new LogProperty(fixture.Create<string>(), fixture.Create<string>());

                var properties = new[]
                {
                    batchableProperty, commonProperty
                };

                //Act
                var batchedLogProperties = BatchedLogPropertiesFactory.CreateBatches(properties);

                //Assert
                batchedLogProperties.Should().HaveCount(2);

                var firstBatch = batchedLogProperties.ElementAt(0);
                var firstBatchActualCommonProperty = firstBatch.First();
                var firstBatchActualBatchedProperty = firstBatch.Last();

                firstBatchActualCommonProperty.Should().BeEquivalentTo(commonProperty);
                firstBatchActualBatchedProperty.Value.Should().BeEquivalentTo(batchableValue.Take(10));


                var secondBatch = batchedLogProperties.ElementAt(1);

                var secondBatchActualCommonProperty = secondBatch.First();
                var secondBatchActualBatchedProperty = secondBatch.Last();

                secondBatchActualCommonProperty.Should().BeEquivalentTo(commonProperty);
                secondBatchActualBatchedProperty.Value.Should().BeEquivalentTo(batchableValue.TakeLast(10));
            }

            [Theory, AutoData]
            public void ShouldReturnOneBatch_WhenNoBachableProperties(
                Fixture fixture)
            {
                //Arrange
                var commonProperty = new LogProperty(fixture.Create<string>(), fixture.Create<string>());

                var properties = new[]
                {
                    commonProperty
                };

                //Act
                var batchedLogProperties = BatchedLogPropertiesFactory.CreateBatches(properties);

                //Assert
                batchedLogProperties.Should().HaveCount(1);

                var firstBatch = batchedLogProperties.ElementAt(0);
                var firstBatchActualCommonProperty = firstBatch.First();

                firstBatchActualCommonProperty.Should().BeEquivalentTo(commonProperty);
            }
        }
    }
}