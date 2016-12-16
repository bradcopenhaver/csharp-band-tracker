using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {
    int _id;
    string _name;

    public Band(string name, int id=0)
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
    public override bool Equals(Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        return (idEquality && nameEquality);
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands ORDER BY name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Band newBand =  new Band(name, id);
        allBands.Add(newBand);
      }
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();

      return allBands;
    }
    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);

      cmd.Parameters.AddWithValue("@BandId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;

      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
      }
      Band foundBand = new Band(foundBandName, foundBandId);
      if(rdr != null) rdr.Close();
      if(conn != null) conn.Close();

      return foundBand;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@name);", conn);

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

      SqlCommand cmd = new SqlCommand("UPDATE bands SET name = @NewName OUTPUT INSERTED.name WHERE id = @BandId;", conn);
      cmd.Parameters.AddWithValue("@NewName", newName);
      cmd.Parameters.AddWithValue("BandId", _id);

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

      SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId; DELETE FROM bands_venues WHERE band_id = @BandId;", conn);
      cmd.Parameters.AddWithValue("@BandId", _id);
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
    public void AddVenue(int venueId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @venueId);", conn);
      cmd.Parameters.AddWithValue("@bandId", _id.ToString());
      cmd.Parameters.AddWithValue("@venueId", venueId.ToString());
      cmd.ExecuteNonQuery();

      if (conn != null) conn.Close();
    }
    public List<Venue> GetVenues()
    {
      List<Venue> bandVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @bandId ORDER BY name;", conn);
      cmd.Parameters.AddWithValue("@bandId", _id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        bandVenues.Add(new Venue(venueName, venueId));
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return bandVenues;
    }
  }
}
