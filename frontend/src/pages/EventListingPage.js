import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS

const EventListingPage = () => {
  const [events, setEvents] = useState([]);
  const [sortBy, setSortBy] = useState('name'); // Default sorting by name
  const [days, setDays] = useState(30); // Default filter for 30 days
  const [isNarrowScreen, setIsNarrowScreen] = useState(false); // Track screen size

  // Detect screen size
  useEffect(() => {
    const mediaQuery = window.matchMedia('(max-width: 768px)'); // Define narrow screen as < 768px
    setIsNarrowScreen(mediaQuery.matches);

    const handleResize = () => setIsNarrowScreen(mediaQuery.matches);
    mediaQuery.addEventListener('change', handleResize);

    return () => mediaQuery.removeEventListener('change', handleResize);
  }, []);

  useEffect(() => {
    // Fetch events from the API on page load or when `days` changes
    fetch(`http://localhost:5047/api/events?days=${days}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        setEvents(data); // Update events state with fetched data
        console.log('Fetched events:', data); // Log the result to the console
      })
      .catch((error) => {
        console.error('Error fetching events:', error); // Log any errors to the console
      });
  }, [days]); // Re-fetch data when `days` changes

  // Sort events based on the selected criteria
  const sortedEvents = [...events].sort((a, b) => {
    if (sortBy === 'name') {
      return a.name.localeCompare(b.name);
    } else if (sortBy === 'startDate') {
      return new Date(a.startsOn) - new Date(b.startsOn);
    }
    return 0;
  });

  return (
    <div className="container mt-4">
      <h1 className="text-center mb-4">Event Listing</h1>

      {/* Dropdown for days */}
      <div className="d-flex justify-content-center mb-3">
        <div className="btn-group">
          <button
            className={`btn ${days === 30 ? 'btn-primary' : 'btn-outline-primary'}`}
            onClick={() => setDays(30)}
          >
            30 Days
          </button>
          <button
            className={`btn ${days === 60 ? 'btn-primary' : 'btn-outline-primary'}`}
            onClick={() => setDays(60)}
          >
            60 Days
          </button>
          <button
            className={`btn ${days === 180 ? 'btn-primary' : 'btn-outline-primary'}`}
            onClick={() => setDays(180)}
          >
            180 Days
          </button>
        </div>
      </div>

      {/* Toggle for sorting */}
      <div className="d-flex justify-content-center mb-4">
        <div className="btn-group">
          <button
            className={`btn ${sortBy === 'name' ? 'btn-primary' : 'btn-outline-primary'}`}
            onClick={() => setSortBy('name')}
          >
            Sort by Name
          </button>
          <button
            className={`btn ${sortBy === 'startDate' ? 'btn-primary' : 'btn-outline-primary'}`}
            onClick={() => setSortBy('startDate')}
          >
            Sort by Start Date
          </button>
        </div>
      </div>

      {/* Conditional Rendering: Table for wide screens, Cards for narrow screens */}
      {isNarrowScreen ? (
        <div className="row">
          {sortedEvents.map((event) => (
            <div className="col-md-4 mb-4" key={event.id}>
              <div className="card h-100">
                <div className="card-body">
                  <h5 className="card-title">{event.name}</h5>
                  <p className="card-text">
                    <strong>Event ID:</strong> {event.id}
                  </p>
                  <p className="card-text">
                    <strong>Start Date:</strong> {new Date(event.startsOn).toLocaleString()}
                  </p>
                  <p className="card-text">
                    <strong>End Date:</strong> {new Date(event.endsOn).toLocaleString()}
                  </p>
                  <p className="card-text">
                    <strong>Location:</strong> {event.location}
                  </p>
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <table className="table table-bordered">
          <thead className="thead-light">
            <tr>
              <th>Event ID</th>
              <th>Event Name</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>Location</th>
            </tr>
          </thead>
          <tbody>
            {sortedEvents.map((event) => (
              <tr key={event.id}>
                <td>{event.id}</td>
                <td>{event.name}</td>
                <td>{new Date(event.startsOn).toLocaleString()}</td>
                <td>{new Date(event.endsOn).toLocaleString()}</td>
                <td>{event.location}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default EventListingPage;