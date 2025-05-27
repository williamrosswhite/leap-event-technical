import { useState } from 'react';

// Import components
import LoadingSpinner from '../components/LoadingSpinner';
import ErrorAlert from '../components/ErrorAlert';
import EventsRenderer from '../components/RenderEvents';

// Import hooks
import useMediaQuery from '../hooks/MediaQueryHandler';
import EventFilters from '../hooks/EventFilters';
import useFetchEvents from '../hooks/useFetchEvents';

// Import utility functions
import sortEvents from '../utils/sortEvents';

// Import styling
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/common.css';
import '../styles/EventListingPage.css';

const EventListingPage = () => {
  const [sortBy, setSortBy] = useState('name'); // Default sorting by name
  const [sortOrder, setSortOrder] = useState('asc'); // Default sorting order is ascending
  const [days, setDays] = useState(30); // Default filter for 30 days

  // Custom hooks
  const { events, loading, error } = useFetchEvents(days);
  const isNarrowScreen = useMediaQuery('(max-width: 768px)');
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

          {/* Conditional rendering, table for desktop cards for mobile */}
          <EventsRenderer
            sortedEvents={sortedEvents}
            isNarrowScreen={isNarrowScreen}
            rightColumnLabel="Location"
          />
        </>
      )}
    </div>
  );
};

export default EventListingPage;