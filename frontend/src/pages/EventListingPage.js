import React, { useState } from 'react';

// import components
import LoadingSpinner from '../components/LoadingSpinner';
import ErrorAlert from '../components/ErrorAlert';
import RenderEvents from '../components/RenderEvents';

// import hooks
import useMediaQuery from '../hooks/MediaQueryHandler';
import EventFilters from '../hooks/EventFilters';
import useFetchEvents from '../hooks/useFetchEvents';

// import utility functions
import sortEvents from '../utils/sortEvents';

// import styling
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/common.css';
import '../styles/EventListingPage.css';

const EventListingPage = () => {
  const [sortBy, setSortBy] = useState('name'); // Default sorting by name
  const [sortOrder, setSortOrder] = useState('asc'); // Default sorting order is ascending
  const [days, setDays] = useState(30); // Default filter for 30 days

  // Use custom hook for fetching events
  const { events, loading, error } = useFetchEvents(days);

  // Use custom hook for media query
  const isNarrowScreen = useMediaQuery('(max-width: 768px)');

  // Sort events based on the selected criteria and order
  const sortedEvents = sortEvents(events, sortBy, sortOrder);

  return (
    <div className="container mt-4">
      <h1 className="page-header">Event Listing</h1>

      {/* Error Handling */}
      {error && <ErrorAlert message={error} />}

      {/* Loading State */}
      {loading && <LoadingSpinner />}

      {/* Dropdown for days */}
      {!loading && !error && (
        <>
          <EventFilters
            days={days}
            setDays={setDays}
            sortBy={sortBy}
            setSortBy={setSortBy}
            sortOrder={sortOrder}
            setSortOrder={setSortOrder}
          />

          {/* Conditional Rendering: Table for wide screens, Cards for narrow screens */}
          <RenderEvents sortedEvents={sortedEvents} isNarrowScreen={isNarrowScreen} />
        </>
      )}
    </div>
  );
};

export default EventListingPage;