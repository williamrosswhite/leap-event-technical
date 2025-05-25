import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS

const SalesSummaryPage = () => {
  const [salesByTickets, setSalesByTickets] = useState([]);
  const [salesByRevenue, setSalesByRevenue] = useState([]);
  const [error, setError] = useState(null); // Track API errors
  const [activeTab, setActiveTab] = useState('tickets'); // Track active tab
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
    // Fetch top 5 events by ticket count
    fetch('http://localhost:5047/api/tickets/top-sales')
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        setSalesByTickets(data); // Update state with ticket count data
        console.log('Fetched top 5 events by ticket count:', data);
      })
      .catch((error) => {
        console.error('Error fetching top 5 events by ticket count:', error);
        setError('Failed to fetch top 5 events by ticket count. Please try again later.');
      });

    // Fetch top 5 events by revenue
    fetch('http://localhost:5047/api/tickets/top-revenue')
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        setSalesByRevenue(data); // Update state with revenue data
        console.log('Fetched top 5 events by revenue:', data);
      })
      .catch((error) => {
        console.error('Error fetching top 5 events by revenue:', error);
        setError('Failed to fetch top 5 events by revenue. Please try again later.');
      });
  }, []); // Fetch data only once on component mount

  return (
    <div className="container mt-4">
      <h1 className="text-center mb-4">Sales Summary</h1>

      {/* Error Handling */}
      {error ? (
        <div className="alert alert-danger text-center" role="alert">
          {error}
        </div>
      ) : (
        <>
          {/* Tabs for toggling between ticket count and revenue */}
          <div className="d-flex justify-content-center mb-3">
            <div className="btn-group">
              <button
                className={`btn ${activeTab === 'tickets' ? 'btn-primary' : 'btn-outline-primary'}`}
                onClick={() => setActiveTab('tickets')}
              >
                Top 5 by Tickets Sold
              </button>
              <button
                className={`btn ${activeTab === 'revenue' ? 'btn-primary' : 'btn-outline-primary'}`}
                onClick={() => setActiveTab('revenue')}
              >
                Top 5 by Revenue
              </button>
            </div>
          </div>

          {/* Conditional Rendering: Table for wide screens, Cards for narrow screens */}
          {activeTab === 'tickets' && (
            isNarrowScreen ? (
              <div className="row">
                {salesByTickets.map((event) => (
                  <div className="col-md-4 mb-4" key={event.eventId}>
                    <div className="card h-100">
                      <div className="card-body">
                        <h5 className="card-title">{event.eventName}</h5>
                        <p className="card-text">
                          <strong>Event ID:</strong> {event.eventId}
                        </p>
                        <p className="card-text">
                          <strong>Start Date:</strong> {new Date(event.eventStartDate).toLocaleString()}
                        </p>
                        <p className="card-text">
                          <strong>End Date:</strong> {new Date(event.eventEndDate).toLocaleString()}
                        </p>
                        <p className="card-text">
                          <strong>Tickets Sold:</strong> {event.ticketCount !== null ? event.ticketCount.toLocaleString() : 'N/A'}
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
                    <th>Tickets Sold</th>
                  </tr>
                </thead>
                <tbody>
                  {salesByTickets.map((event) => (
                    <tr key={event.eventId}>
                      <td>{event.eventId}</td>
                      <td>{event.eventName}</td>
                      <td>{new Date(event.eventStartDate).toLocaleString()}</td>
                      <td>{new Date(event.eventEndDate).toLocaleString()}</td>
                      <td>{event.ticketCount !== null ? event.ticketCount.toLocaleString() : 'N/A'}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )
          )}

          {activeTab === 'revenue' && (
            isNarrowScreen ? (
              <div className="row">
                {salesByRevenue.map((event) => (
                  <div className="col-md-4 mb-4" key={event.eventId}>
                    <div className="card h-100">
                      <div className="card-body">
                        <h5 className="card-title">{event.eventName}</h5>
                        <p className="card-text">
                          <strong>Event ID:</strong> {event.eventId}
                        </p>
                        <p className="card-text">
                          <strong>Start Date:</strong> {new Date(event.eventStartDate).toLocaleString()}
                        </p>
                        <p className="card-text">
                          <strong>End Date:</strong> {new Date(event.eventEndDate).toLocaleString()}
                        </p>
                        <p className="card-text">
                          <strong>Total Revenue:</strong> ${event.totalSales.toLocaleString()}
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
                    <th>Total Revenue</th>
                  </tr>
                </thead>
                <tbody>
                  {salesByRevenue.map((event) => (
                    <tr key={event.eventId}>
                      <td>{event.eventId}</td>
                      <td>{event.eventName}</td>
                      <td>{new Date(event.eventStartDate).toLocaleString()}</td>
                      <td>{new Date(event.eventEndDate).toLocaleString()}</td>
                      <td>${event.totalSales.toLocaleString()}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )
          )}
        </>
      )}
    </div>
  );
};

export default SalesSummaryPage;