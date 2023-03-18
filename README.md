## Required Projects
The solution is a client-server solution consisting of multiple projects as outlined in the course. The Presentation Layer portion is an ASP.NET Web Application (**.NET Core** Razor Pages). The BLL, Entity classes and DAL portions of the system are placed in a separate class library project.

## The StarTED Database
The database supplied for this lab is an SQL Server database named "StarTED". The following is a sample of the connection string that is used for the Presentation Layer.

```json
  "StarTEDDb": "Server=.;Database=StarTED;Trusted_Connection=true;MultipleActiveResultSets=true",
   "StarTEDRemoteVpnDmitDB" : "Server=DMIT-Capstone1.ad.sast.ca;Database=CPSC1517_1221_yourSection_yourNaitUserName;User Id=yourNaitUsername;Password=RemotePassword.yourNaitStudentId;TrustServerCertificate=True;MultipleActiveResultSets=true",
    "StarTEDRemoteDmitDB" : "Server=CAPSTONE1.dmit.sast.ca;Database=CPSC1517_1221_yourSection_yourNaitUserName;User Id=yourNaitUsername;Password=RemotePassword.yourNaitStudentId;TrustServerCertificate=True;MultipleActiveResultSets=true",
```

## The Forms
There are two pages for the core functionality of this project (described below). All of these pages must use the same [layout](#layout-and-styling).

### Layout and Styling
The styling approach for this site uses custom css. The layout page contains: 

* **Site Navigation** – Links to all the pages in the web application.
* **Scenario Title** – The number and name of the scenario (e.g.: G1 – Reservations by Group).
* **Student Name** – My first and last name.

### Default Page

The **Index** page contains the following elements (each with their own heading):

- **Deliverable Descriptions** – A brief description (one or two paragraphs) of the `Query` and `CRUD` page's requirements in the project, identifying the name of the page and its purpose, along with any unique constraints or characteristics of the page's behaviour.
- **Known Bugs** – A bulleted list of all the known bugs and incomplete portions of the lab.
- **Entity Relationship Diagram** - The ERD diagram.
- **Site Styling Decision** - Indicate what [site-wide styling](#layout-and-styling) you are using in your website.
----
## Query – Search/Filter & Display Results
This page displays multiple rows of data in an HTML table, with appropriate links from this page to the CRUD page for editing related data on this page or adding new data. This page also implement's pagination, with the page size being set to 10 to 25 rows per page. 
___

## CRUD – Single Item CRUD
On this page: 
* **Insert** new rows of data into a table
* **Update** an existing row of data in a table
* **Delete** (or mark as inactive/not current) a row of data in a table
* **Cancel** editing (this should return the user to the `Query` page)

### Handling Foreign Keys With Large Data Sets
For many of the scenarios, foreign key information will have to be handled in a search/filter manner. The reason for this is because there are far too many rows of data to put in a single drop-down control. For example, with thousands of *Students* in the database, it is impractical to fill a drop-down and expect a user to find the student they wish to edit. In these situations, a two-step selection process makes the form more manageable by the user. 

### Processing Delete
Depending on the scenario and the table, the Delete functionality may not be a physical removal of a row of data. This is because some tables have triggers or other constraints that prevent the removal of existing rows. In these cases where a row cannot be deleted (excluding foreign key constraints preventing removal), the table will have some means of flagging the row as being inactive or not current. 
