import { useState, useEffect } from 'react';

const useFetchSalesData = () => {
  const [salesByTickets, setSalesByTickets] = useState([]);
  const [salesByRevenue, setSalesByRevenue] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);

      try {
        // Fetch top 5 events by ticket count
        // Obviously would secure this port if I kept working on this
        const ticketsResponse = await fetch('http://localhost:5047/api/tickets/top-sales');
        if (!ticketsResponse.ok) {
          throw new Error(`Failed to fetch top 5 events by ticket count. Status: ${ticketsResponse.status}`);
        }
        const ticketsData = await ticketsResponse.json();
        setSalesByTickets(
          ticketsData.map((event) => ({
            id: event.eventId,
            name: event.eventName,
            startsOn: event.eventStartDate,
            endsOn: event.eventEndDate,
            location: `${event.ticketCount.toLocaleString()} Tickets Sold`,
          }))
        );

        // Fetch top 5 events by revenue
        // Obviously would secure this port if I kept working on this
        const revenueResponse = await fetch('http://localhost:5047/api/tickets/top-revenue');
        if (!revenueResponse.ok) {
          throw new Error(`Failed to fetch top 5 events by revenue. Status: ${revenueResponse.status}`);
        }
        const revenueData = await revenueResponse.json();
        setSalesByRevenue(
          revenueData.map((event) => ({
            id: event.eventId,
            name: event.eventName,
            startsOn: event.eventStartDate,
            endsOn: event.eventEndDate,
            location: event.totalSales,
          }))
        );
      } catch (err) {
        console.error('Error fetching data:', err);
        setError('Failed to load data. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return { salesByTickets, salesByRevenue, loading, error };
};

export default useFetchSalesData;