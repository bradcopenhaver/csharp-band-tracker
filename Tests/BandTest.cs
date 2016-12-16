using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public void Dispose()
    {
      Band.DeleteAll();
    }
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Equals_EqualOverride_True()
    {
      //Arrange and Act
      Band firstBand = new Band("Three Days Grace");
      Band secondBand = new Band("Three Days Grace");
      //Assert
      Assert.Equal(firstBand,secondBand);
    }
    [Fact]
    public void Save_SavesBandToDatabase_true()
    {
      //Arrange
      Band newBand = new Band("Sublime");
      //Act
      newBand.Save();
      List<Band> allBands = Band.GetAll();
      //Assert
      Assert.Equal(newBand, allBands[0]);
    }
    [Fact]
    public void Find_FindsBandById_true()
    {
      //Arrange
      Band newBand = new Band("Wilson Phillips");
      newBand.Save();
      //Act
      Band foundBand = Band.Find(newBand.GetId());
      //Assert
      Assert.Equal(newBand, foundBand);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      //Arrange
      Band newBand = new Band("Three Dog Night");
      newBand.Save();
      //Act
      newBand.Edit("Dog's Eye View");
      Band foundBand = Band.Find(newBand.GetId());

      //Assert
      Assert.Equal("Dog's Eye View", foundBand.GetName());
    }
    [Fact]
    public void Delete_DeletesBandFromDB_true()
    {
      //Arrange
      Band newBand1 = new Band("KISS");
      Band newBand2 = new Band("Metallica");
      newBand1.Save();
      newBand2.Save();
      //Act
      newBand1.Delete();
      List<Band> result = Band.GetAll();
      List<Band> expectedResult = new List<Band> {newBand2};
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
