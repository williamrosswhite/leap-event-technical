import { useState, useEffect } from 'react';

const useFetchEvents = (days) => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEvents = async () => {
      setLoading(true);
      setError(null);

      try {
        // Obviously would secure this port if I kept working on this
        const response = await fetch(`http://localhost:5047/api/events?days=${days}`);
        if (!response.ok) {
          throw new Error(`Failed to fetch events. Status: ${response.status}`);
        }

        const data = await response.json();
        setEvents(data);
      } catch (err) {
        setError('Failed to load events. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchEvents();
  }, [days]);

  return { events, loading, error };
};

export default useFetchEvents;