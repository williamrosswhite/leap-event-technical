import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS

// Import custom components
import LoadingSpinner from '../components/LoadingSpinner';
import ErrorAlert from '../components/ErrorAlert';
import ButtonGroup from '../components/ButtonGroup';
import SalesDataRenderer from '../components/SalesDataRenderer';
import EventTable from '../components/EventTable';

// Import custom hooks
import useMediaQuery from '../hooks/MediaQueryHandler';
import useFetchSalesData from '../hooks/useFetchSalesData';

// Import styling
import '../styles/common.css'; // Import shared styles
import '../styles/SalesSummaryPage.css'; // Import page-specific styles

const SalesSummaryPage = () => {
  const { salesByTickets, salesByRevenue, loading, error } = useFetchSalesData();
  const [activeTab, setActiveTab] = React.useState('tickets'); // Track active tab
  const isNarrowScreen = useMediaQuery('(max-width: 768px)'); // Detect screen size

  // Determine the right column title based on the active tab
  const rightColumnLabel = activeTab === 'tickets' ? 'Tickets Sold' : 'Total Revenue';

  return (
    <div className="container">
      <h1 className="page-header">Sales Summary</h1>

      {/* Error Handling */}
      {error && <ErrorAlert message={error} />}

      {/* Loading State */}
      {loading && <LoadingSpinner />}

      {/* Main Content */}
      {!loading && !error && (
        <>
          {/* Tabs for toggling between ticket count and revenue */}
          <div className="sales-summary-tabs d-flex justify-content-center">
            <ButtonGroup
              options={[
                { value: 'tickets', label: 'Top 5 by Tickets Sold' },
                { value: 'revenue', label: 'Top 5 by Revenue' },
              ]}
              selected={activeTab}
              onClick={setActiveTab}
            />
          </div>

          {/* Conditional Rendering */}
          <SalesDataRenderer
            data={activeTab === 'tickets' ? salesByTickets : salesByRevenue}
            isNarrowScreen={isNarrowScreen}
            cardLocationKey="location"
            renderTable={(events) => (
              <EventTable
                events={events}
                rightColumnLabel={rightColumnLabel} // Pass dynamic column title
              />
            )}
          />
        </>
      )}
    </div>
  );
};

export default SalesSummaryPage;