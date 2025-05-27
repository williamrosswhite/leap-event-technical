# Leap Event Technology Assessment Project

### Database preparation
- Database setup:
	- Corrected database file is included in the repo and should work out of the box
	- I changed the datetime field type in DB Browser for SQLite to text, no script required
		- There is no datetime type in SQLite, and datetime could not be validated by NHibernate.
	- Based on the types of lookups needing to be done quickly and repeatedly, I created the following indexes in the provided .db file:
		- ``CREATE INDEX idx_events_startson ON Events(StartsOn);``
		- ``CREATE INDEX idx_events_startson_endson ON Events(StartsOn, EndsOn);``
		- ``CREATE INDEX idx_tickets_eventid ON TicketSales(EventId);``
		- ``CREATE INDEX idx_tickets_eventid_purchasedate ON TicketSales(EventId, PurchaseDate);``
		- ``CREATE INDEX idx_tickets_purchasedate ON TicketSales(PurchaseDate);``

### How to run this project:

- Ensure you have the [.NET SDK](https://dotnet.microsoft.com/en-us/download) and [Node](https://nodejs.org/en/download) installed on your computer
- Clone the repo: [https://github.com/williamrosswhite/leap-event-technical.git](https://github.com/williamrosswhite/leap-event-technical.git)
- Open a terminal and cd into leap-event-technical 
- cd into /backend
- Run ``dotnet restore``
- Run ``dotnet build``
- Run ``dotnet run``
- Swagger page will launch.
- Open a second terminal in leap-event-technical root
- cd into /frontend
- run ``npm install``
- run ``npm start``
- Application page will launch.

### How to run tests:
- Open a terminal into leap-event-technical root
- Run ``dotnet build``
- Run ``dotnet test``

### Things to point out:
- I Chose MSTest because I wasn’t familiar with either SQLite type (worked mostly with Entity Framework), and MSTest appears more friendly to a newcomer friendly, though NUnit seems more robust and configurable and might choose for a production project.
- I had to change database’s datetime to text because there is no datetime object in SQLite, and I couldn’t validate the schema in NHibernate otherwise.
- I had concern about the dates.  They weren’t listed as UTC in the database (missing the Z).  I could have:
	- Adjusted the database to alter all dates to be UTC, but this makes unwarranted assumptions.
	- Rendered the dates as is, but this assumes dates are in time zone of event (what I went with).
	- Attempted to automatically parse the location to provide a time zone to present the date/time with, but this is well out of scope and would assume that the given times were supposed to be UTC.
	- Ultimately in the real world I would discuss with the team at next meeting which approach to go with.

### Things I wish I’d had more time to do:
- Really wish I’d had the chance to make the backend service functions asynchronous but was held back by unfamiliarity with NHibernate and how to handle async in the tests.
- Look into other ways I could have increased performance of sorting algorithm.
- Improve aesthetics of pages.
- I focused on solid functionality instead of aesthetics since that was the focus of the assignment.
- Match font and background video of Leap Event Technology Page.
- Other ways to make it look associated with Leap Event Technology page.
- Use properly separated ports and publish it on azure.
- Implement .env files to keep ports and any future logins safe, and easy to transfer to something like Azure Key Vault later on.
- Add front-end unit tests.
- Investigate NHibernate’s second level caching for optimizing performance.
- Investigate NHibernate’s eager loading to optimize response by reducing number of database queries.
