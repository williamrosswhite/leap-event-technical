import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import '../styles/common.css'; // Import shared styles
import '../styles/SalesSummaryPage.css'; // Import page-specific styles

const SalesSummaryPage = () => {
  const [salesByTickets, setSalesByTickets] = useState([]);
  const [salesByRevenue, setSalesByRevenue] = useState([]);
  const [error, setError] = useState(null); // Track API errors
  const [loading, setLoading] = useState(false); // Track loading state
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

  // Fetch data for tickets and revenue
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true); // Start loading
      setError(null); // Reset error state

      try {
        // Fetch top 5 events by ticket count
        const ticketsResponse = await fetch('http://localhost:5047/api/tickets/top-sales');
        if (!ticketsResponse.ok) {
          throw new Error(`Failed to fetch top 5 events by ticket count. Status: ${ticketsResponse.status}`);
        }
        const ticketsData = await ticketsResponse.json();
        setSalesByTickets(ticketsData);

        // Fetch top 5 events by revenue
        const revenueResponse = await fetch('http://localhost:5047/api/tickets/top-revenue');
        if (!revenueResponse.ok) {
          throw new Error(`Failed to fetch top 5 events by revenue. Status: ${revenueResponse.status}`);
        }
        const revenueData = await revenueResponse.json();
        setSalesByRevenue(revenueData);
      } catch (err) {
        console.error('Error fetching data:', err); // Log error for debugging
        setError('Failed to load data. Please try again later.'); // Set user-friendly error message
      } finally {
        setLoading(false); // Stop loading
      }
    };

    fetchData();
  }, []); // Fetch data only once on component mount

  return (
    <div className="container">
      <h1 className="page-header">Sales Summary</h1>

      {/* Error Handling */}
      {error && (
        <div className="alert alert-danger text-center" role="alert">
          {error}
        </div>
      )}

      {/* Loading State */}
      {loading && (
        <div className="text-center">
          <div className="spinner-border text-primary" role="status">
            <span className="sr-only">Loading...</span>
          </div>
        </div>
      )}

      {/* Main Content */}
      {!loading && !error && (
        <>
          {/* Tabs for toggling between ticket count and revenue */}
          <div className="sales-summary-tabs d-flex justify-content-center">
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