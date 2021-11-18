using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace LogBuilder.Tests
{
    public class LogBuilderTests
    {
        public class Constructor
        {
            [Fact]
            public void ShouldReturnExpectedMessage_WhenOnlyMessageIsProvided()
            {
                //Arrange
                const string inputMessage = "test message";
                var test = new List<KeyValuePair<string, object>>();

                //Act
                var builder = new LogBuilder(inputMessage);
                var logArgs = builder.ToList();

                //Assert
                inputMessage.Should().Be(LogBuilder.Formatter(builder, new Exception()));
                logArgs.Count.Should().Be(0);
            }

            [Fact]
            public void ShouldReturnExpectedMessageAndLogArgs_WhenBothAreProvided()
            {
                //Arrange
                var inputLogs = new LogProperty("fieldName", "fieldValue");
                const string inputMessage = "test message";
                var expectedLogArgs = new List<LogProperty>
                {
                    new (inputLogs.Key, inputLogs.Value)
                };

                //Act
                var builder = new LogBuilder(inputMessage, inputLogs);
                var logArgs = builder.GetLogProperties();

                //Assert
                inputMessage.Should().Be(LogBuilder.Formatter(builder, new Exception()));
                expectedLogArgs.Should().Equal(logArgs);
            }

            [Fact]
            public void ShouldReturnExpectedFormatter_WhenMessageFormatIsProvided()
            {
                //Arrange
                const string inputMessage = "test message";

                //Act
                var builder = new LogBuilder(inputMessage);

                //Assert
                inputMessage.Should().Be(LogBuilder.Formatter.Invoke(builder, null));
            }
        }

        public class AppendArgs
        {
            [Fact]
            public void ShouldReturnExpectedLogArgs_WhenWithArgsInvoked()
            {
                //Arrange
                var inputLogs = new LogProperty("fieldName", "fieldValue");
                const string inputMessage = "test message";
                var builder = new LogBuilder(inputMessage);
                var expectedLogArgs = new List<LogProperty>
                {
                    new (inputLogs.Key, inputLogs.Value)
                };

                //Act
                var result = builder.WithProperties(inputLogs);
                var logArgs = result.GetLogProperties();

                //Assert
                expectedLogArgs.Should().Equal(logArgs);
            }
        }
    }
}