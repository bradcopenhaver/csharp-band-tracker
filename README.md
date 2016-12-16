# Band Venue Tracker

#### This web app tracks bands and the venues where they have performed. {December 2016}

#### By **Brad Copenhaver**

## Description
This program uses a local database to keep track of different bands and the venues where they have performed. Bands, venues, and their relationships are entered manually and may be edited and deleted.

### Specifications
_This program will..._

1. Save a record of info for an individual band or venue.
 * Input: Band: Third Eye Blind
 * Output: Band: {1, Third Eye Blind}
2. Update information for any individual record.
 * Input: Roseland Ballroom -> Roseland Theater
 * Output: Venue: {1, Roseland Ballroom} -> {1, Roseland Theater}
3. Save a record of any venue at which a band has played.
 * Input: Third Eye Blind Venues
 * Output: Crystal Ballroom, Roseland Theater, ...
4. Delete any individual band or venue and all of that individual's associations in the database.
 * Input: Delete Matchbox 20
 * Output: Band: {2, Matchbox 20} -> {}

## Setup/Installation Requirements

1. Clone this GitHub repository.
2. From the command prompt, run '>SqlLocalDb.exe c MSSQLLocalDB -s' to create an instance of LocalDB.
3. Run the command '>sqlcmd -S "(localdb)\\MSSQLLocalDB"' and run the following SQL commands to create the local database and tables and the test database:

        >CREATE DATABASE band_tracker
        >GO
        >USE band_tracker
        >GO
        >CREATE TABLE bands(
	    > id INT IDENTITY(1,1),
        > name VARCHAR(255)
        >)
        >GO
        >CREATE TABLE venues(
        > id INT IDENTITY(1,1),
        > name VARCHAR(255)
        >)
        >GO
        >CREATE TABLE bands_venues(
        > id INT IDENTITY(1,1),
        > band_id INT,
        > venue_id INT
        >)
        >GO
        >CREATE DATABASE band_tracker_test
        >GO
        >USE band_tracker_test
        >GO
        >CREATE TABLE bands(
	    > id INT IDENTITY(1,1),
        > name VARCHAR(255)
        >)
        >GO
        >CREATE TABLE venues(
        > id INT IDENTITY(1,1),
        > name VARCHAR(255)
        >)
        >GO
        >CREATE TABLE bands_venues(
        > id INT IDENTITY(1,1),
        > band_id INT,
        > venue_id INT
        >)
        >GO
4. Navigate to the repository in terminal and run the command >dnu restore
5. In the same location, create a local server by running the command >dnx kestrel
6. Open a web browser and navigate to localhost:5004 to view the app.
7. Tests can be run with the command >dnx test

## Known Bugs

None yet, mostly due to lack of testing.

## Possible future version features

Add more properties to bands and venues.
Track individual performances instead of just associations.

## Support and contact details

If you have questions or comments, contact the author at bradcopenhaver@gmail.com

## Technologies Used

* C#
* SQL
* Nancy framework
* Razor view engine
* html/css
* Bootstrap

### License

This project is licensed under the MIT license.

Copyright (c) 2016 **Brad Copenhaver**
