import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

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
import '../styles/common.css';
import '../styles/SalesSummaryPage.css';

const SalesSummaryPage = () => {
  const { salesByTickets, salesByRevenue, loading, error } = useFetchSalesData();
  const [activeTab, setActiveTab] = React.useState('tickets'); // Default to tickets tab
  const isNarrowScreen = useMediaQuery('(max-width: 768px)'); 

  // Set right column title based on active tab
  const rightColumnLabel = activeTab === 'tickets' ? 'Tickets Sold' : 'Total Revenue';

  return (
    <div className="container">
      <h1 className="page-header">Sales Summary</h1>

      {/* Error Handling */}
      {error && <ErrorAlert message={error} />}

      {/* Loading State */}
      {loading && <LoadingSpinner />}

      {!loading && !error && (
        <>
          {/* Toggle between between ticket count and revenue */}
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

          {/* Conditional rendering, table for desktop cards for mobile */}
          <SalesDataRenderer
            data={activeTab === 'tickets' ? salesByTickets : salesByRevenue}
            isNarrowScreen={isNarrowScreen}
            cardLocationKey="location"
            renderTable={(events) => (
              <EventTable
                events={events}
                rightColumnLabel={rightColumnLabel} // Dynamic column title
              />
            )}
          />
        </>
      )}
    </div>
  );
};

export default SalesSummaryPage;