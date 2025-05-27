# Leap Event Technology Assessment Project

### How to run this project:

- Clone and repo: [https://github.com/williamrosswhite/leap-event-technical.git](https://github.com/williamrosswhite/leap-event-technical.git)
- Open a terminal and cd into leap-event-technical
- cd into /backend
- run dotnet restore
- run dotnet run
- swagger page will launch
- open a second terminal in leap-event-technical root
- cd into /frontend
- run npm install
- run npm start
- application page will launch

### How to run tests:

- open a terminal into leap-event-technical root
- run dotnet build
- run dotnet test


### Things to point out:
- I Chose MSTest because I wasn’t familiar with either sqlite type (worked mostly with Entity Framework), and MSTest appears more friendly to a newcomer friendly, though NUnit seems more robust and configurable and might choose for a production project.
- I had to change database’s datetime to text because there is no datetime object in sqlite, and I couldn’t validate the schema in NHibernate otherwise.
- I had concern about the dates.  They weren’t listed as utc in the database (missing the Z).  I could have:
	- Adjusted the database to alter all dates to be UTC, but this makes unwarranted assumptions
	- Rendered the dates as is, but this assumes dates are in time zone of event (what I went with)
	- Attempted to automatically parse the location to provide a time zone to present the date/time with, but this is well out of scope, and would assume that the given times were supposed to be UTC
	- Ultimately in the real world I would discuss with the team at next meeting how which approach to go with


### Things I wish I’d had more time to do:

- Really wish I’d had the chance to make the backend service functions asynchronous, but was held back by unfamiliarity with NHibernate and how to handle async in the tests
- Implement pagination to save load time
- Look into other ways I could have increased performance of sorting algorithm
- Consider other ways to speed up performance
- Improve aesthetics of pages
	- I focused on solid functionality instead of aesthetics since that was the focus of the assignment
	- Match font of Leap Event Technology Page
	- Use same video background
	- Other ways to make it look associated with Leap Event Technology page
- Use properly separated ports and publish it on azure
- Implement .env files to keep ports safe (and any future credentials)
- Add front end unit tests
- Investigate NHibernate’s second level caching for optimizing performance
- Investigate NHibernate’s eager loading to optimize respone by reducing number of database queries