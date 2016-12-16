using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public void Dispose()
    {
      Venue.DeleteAll();
    }
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Equals_EqualOverride_True()
    {
      //Arrange and Act
      Venue firstVenue = new Venue("Wonder Ballroom");
      Venue secondVenue = new Venue("Wonder Ballroom");
      //Assert
      Assert.Equal(firstVenue,secondVenue);
    }
    [Fact]
    public void Save_SavesVenueToDatabase_true()
    {
      //Arrange
      Venue newVenue = new Venue("Doug Fir");
      //Act
      newVenue.Save();
      List<Venue> allVenues = Venue.GetAll();
      //Assert
      Assert.Equal(newVenue, allVenues[0]);
    }
    [Fact]
    public void Find_FindsVenueById_true()
    {
      //Arrange
      Venue newVenue = new Venue("CBGB");
      newVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(newVenue.GetId());
      //Assert
      Assert.Equal(newVenue, foundVenue);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      //Arrange
      Venue newVenue = new Venue("CBGBBQ");
      newVenue.Save();
      //Act
      newVenue.Edit("CBGB");
      Venue foundVenue = Venue.Find(newVenue.GetId());

      //Assert
      Assert.Equal("CBGB", foundVenue.GetName());
    }
    [Fact]
    public void Delete_DeletesVenueFromDB_true()
    {
      //Arrange
      Venue newVenue1 = new Venue("Madison Square Garden");
      Venue newVenue2 = new Venue("The Goodfoot");
      newVenue1.Save();
      newVenue2.Save();
      //Act
      newVenue1.Delete();
      List<Venue> result = Venue.GetAll();
      List<Venue> expectedResult = new List<Venue> {newVenue2};
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void AddGetBands_AddsAndGetsBandsForVenue_true()
    {
      //Arrange
      Venue newVenue = new Venue("The Waypost");
      Band newBand = new Band("The Heggs");
      Band newBand1 = new Band("Local H");
      newVenue.Save();
      newBand.Save();
      newBand1.Save();
      List<Band> expectedResult = new List<Band>{newBand};
      //Act
      newVenue.AddBand(newBand.GetId());
      List<Band> result = newVenue.GetBands();
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
