using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    int _id;
    string _name;

    public Venue(string name, int id=0)
    {
      _id = id;
      _name = name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public override bool Equals(Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue =  new Venue(name, id);
        allVenues.Add(newVenue);
      }
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();

      return allVenues;
    }
    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);

      cmd.Parameters.AddWithValue("@VenueId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();

      return foundVenue;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@name);", conn);

      cmd.Parameters.AddWithValue("@name", _name);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();
    }
    public void Edit(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);
      cmd.Parameters.AddWithValue("@NewName", newName);
      cmd.Parameters.AddWithValue("VenueId", _id);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;", conn);
      cmd.Parameters.AddWithValue("@VenueId", _id);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
    public void AddBand(int bandId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);", conn);
      cmd.Parameters.AddWithValue("@bandId", bandId.ToString());
      cmd.Parameters.AddWithValue("@venueId", _id.ToString());
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
    public List<Band> GetBands()
    {
      List<Band> venueBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @venueId;", conn);
      cmd.Parameters.AddWithValue("@venueId", _id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        venueBands.Add(new Band(bandName, bandId));
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return venueBands;
    }
  }
}
