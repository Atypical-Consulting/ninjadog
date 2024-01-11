// // Licensed to the.NET Foundation under one or more agreements.
// // The.NET Foundation licenses this file to you under the MIT license.
//
// using Moq;
// using Ninjadog.Templates;
// using Ninjadog.Templates.CrudWebAPI.Template.Contracts.Data;
// using Snapshooter.Xunit;
//
// namespace Ninjadog.Tests;
//
// public class NinjadogTemplateEngineTests
// {
//     [Fact]
//     public void GenerateCode_ShouldCreateExpectedOutput()
//     {
//         // Arrange
//         var templateFileFactory = new Mock<ITemplateFileFactory>();
//         templateFileFactory
//             .Setup(f => f.Create(It.IsAny<TemplateContext>()))
//             .Returns(new DtoTemplate()); // Use a real or mock instance as appropriate
//
//         var engine = new NinjadogTemplateEngine(templateFileFactory.Object);
//         var context = new TemplateContext(null, null);
//
//         // Act
//         var result = engine.GenerateCode(context);
//
//         // Assert
//         Snapshot.Match(result);
//     }
// }
