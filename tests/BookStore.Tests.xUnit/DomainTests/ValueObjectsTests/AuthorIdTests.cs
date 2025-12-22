using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.xUnit.DomainTests.ValueObjectsTests
{
    public static class AuthorIdTests
    {
        public static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void AuthorId_ImplementsIStronglyTypedIdInterface()
            {
                Type authorIdType = typeof(AuthorId);
                Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<Guid>);
                
                Assert.True(stronglyTypedIdInterfaceType.IsAssignableFrom(authorIdType),
                    $"{authorIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
            }

            [Fact]
            public void NewId_ShouldReturnValidAuthorId()
            {
                AuthorId newAuthorId = AuthorId.NewId();
                
                Assert.NotEqual(AuthorId.Empty, newAuthorId);
                Assert.True(newAuthorId.HasValue);
            }

            [Fact]
            public void Empty_ShouldReturnAuthorIdWithEmptyGuid()
            {
                AuthorId emptyAuthorId = AuthorId.Empty;
                
                Assert.Equal(Guid.Empty, emptyAuthorId.Value);
                Assert.False(emptyAuthorId.HasValue);
            }

            [Fact]
            public void Constructor_WithoutParameters_ShouldCreateEmptyAuthorId()
            {
                AuthorId authorId = new();
                
                Assert.Equal(Guid.Empty, authorId.Value);
                Assert.False(authorId.HasValue);
            }

            [Fact]
            public void Constructor_WithGuidParameter_ShouldCreateAuthorIdWithGivenValue()
            {
                Guid guid = Guid.NewGuid();
                AuthorId authorId = new(guid);
                
                Assert.Equal(guid, authorId.Value);
                Assert.True(authorId.HasValue);
            }

            [Fact]
            public void Constructor_WithEmptyGuid_ShouldThrowInvalidGuidException()
            {
                Guid emptyGuid = Guid.Empty;
                
                Assert.Throws<InvalidGuidException>(() => new AuthorId(emptyGuid));
            }

            [Fact]
            public void Constructor_WithStringParameter_ShouldCreateAuthorIdWithGivenValue()
            {
                Guid guid = Guid.NewGuid();
                string guidStr = guid.ToString();
                AuthorId authorId = new(guidStr);
                
                Assert.Equal(guid, authorId.Value);
                Assert.True(authorId.HasValue);
            }

            [Fact]
            public void Constructor_WithInvalidGuidString_ShouldThrowInvalidGuidException()
            {
                string invalidGuidStr = _faker.Random.Word();
                
                Assert.Throws<InvalidGuidException>(() => new AuthorId(invalidGuidStr));
            }

            [Fact]
            public void ToString_ShouldReturnGuidStringRepresentation()
            {
                Guid guid = Guid.NewGuid();
                AuthorId authorId = new(guid);
                
                Assert.Equal(guid.ToString(), authorId.ToString());
            }

            [Fact]
            public void AuthorId_EqualityCheck_ShouldBeTrueForSameValues()
            {
                Guid guid = Guid.NewGuid();

                AuthorId authorId1 = new(guid);
                AuthorId authorId2 = new(guid);

                Assert.Equal(authorId1, authorId2);
            }

            [Fact]
            public void AuthorId_EqualityCheck_ShouldBeFalseForDifferentValues()
            {
                AuthorId authorId1 = new(Guid.NewGuid());
                AuthorId authorId2 = new(Guid.NewGuid());

                Assert.NotEqual(authorId1, authorId2);
            }

            [Fact]
            public void AuthorId_HashCode_ShouldBeSameForSameValues()
            {
                Guid guid = Guid.NewGuid();

                AuthorId authorId1 = new(guid);
                AuthorId authorId2 = new(guid);

                Assert.Equal(authorId1.GetHashCode(), authorId2.GetHashCode());
            }

            [Fact]
            public void AuthorId_HashCode_ShouldBeDifferentForDifferentValues()
            {
                AuthorId authorId1 = new(Guid.NewGuid());
                AuthorId authorId2 = new(Guid.NewGuid());
                
                Assert.NotEqual(authorId1.GetHashCode(), authorId2.GetHashCode());
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void AuthorId_ShouldImplementIStronglyTypedIdInterface()
            {
                Type authorIdType = typeof(AuthorId);
                Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<Guid>);

                authorIdType.Should().Implement(stronglyTypedIdInterfaceType,
                    $"{authorIdType.Name} should implement {stronglyTypedIdInterfaceType.Name} interface.");
            }

            [Fact]
            public void NewId_ShouldReturnValidAuthorId()
            {
                AuthorId newAuthorId = AuthorId.NewId();

                newAuthorId.Should().NotBe(AuthorId.Empty);
                newAuthorId.HasValue.Should().BeTrue();
            }

            [Fact]
            public void Empty_ShouldReturnAuthorIdWithEmptyGuid()
            {
                AuthorId emptyAuthorId = AuthorId.Empty;

                emptyAuthorId.Value.Should().Be(Guid.Empty);
                emptyAuthorId.HasValue.Should().BeFalse();
            }

            [Fact]
            public void Constructor_WithoutParameters_ShouldCreateEmptyAuthorId()
            {
                AuthorId authorId = new();

                authorId.Value.Should().Be(Guid.Empty);
                authorId.HasValue.Should().BeFalse();
            }

            [Fact]
            public void Constructor_WithGuidParameter_ShouldCreateAuthorIdWithGivenValue()
            {
                Guid guid = Guid.NewGuid();
                AuthorId authorId = new(guid);
                authorId.Value.Should().Be(guid);
                authorId.HasValue.Should().BeTrue();
            }

            [Fact]
            public void Constructor_WithEmptyGuid_ShouldThrowInvalidGuidException()
            {
                Guid emptyGuid = Guid.Empty;
                Action act = () => new AuthorId(emptyGuid);
                act.Should().ThrowExactly<InvalidGuidException>();
            }

            [Fact]
            public void Constructor_WithStringParameter_ShouldCreateAuthorIdWithGivenValue()
            {
                Guid guid = Guid.NewGuid();
                string guidStr = guid.ToString();
                AuthorId authorId = new(guidStr);
                authorId.Value.Should().Be(guid);
                authorId.HasValue.Should().BeTrue();
            }

            [Fact]
            public void Constructor_WithInvalidGuidString_ShouldThrowInvalidGuidException()
            {
                string invalidGuidStr = _faker.Random.Word();
                Action act = () => new AuthorId(invalidGuidStr);
                act.Should().ThrowExactly<InvalidGuidException>();
            }

            [Fact]
            public void ToString_ShouldReturnGuidStringRepresentation()
            {
                Guid guid = Guid.NewGuid();
                AuthorId authorId = new(guid);
                
                authorId.ToString().Should().Be(guid.ToString());
            }

            [Fact]
            public void AuthorId_EqualityCheck_ShouldBeTrueForSameValues()
            {
                Guid guid = Guid.NewGuid();
                
                AuthorId authorId1 = new(guid);
                AuthorId authorId2 = new(guid);
                
                authorId1.Should().Be(authorId2);
            }

            [Fact]
            public void AuthorId_EqualityCheck_ShouldBeFalseForDifferentValues()
            {
                AuthorId authorId1 = new(Guid.NewGuid());
                AuthorId authorId2 = new(Guid.NewGuid());
                
                authorId1.Should().NotBe(authorId2);
            }

            [Fact]
            public void AuthorId_HashCode_ShouldBeSameForSameValues()
            {
                Guid guid = Guid.NewGuid();
                
                AuthorId authorId1 = new(guid);
                AuthorId authorId2 = new(guid);
                
                authorId1
                    .GetHashCode()
                    .Should()
                    .Be(authorId2.GetHashCode());
            }

            [Fact]
            public void AuthorId_HashCode_ShouldBeDifferentForDifferentValues()
            {
                AuthorId authorId1 = new(Guid.NewGuid());
                AuthorId authorId2 = new(Guid.NewGuid());

                authorId1
                    .GetHashCode()
                    .Should()
                    .NotBe(authorId2.GetHashCode());
            }
        }
    }
}
