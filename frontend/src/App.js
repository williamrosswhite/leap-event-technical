import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import Navbar from './components/Navbar';
import EventListingPage from './pages/EventListingPage';
import SalesSummaryPage from './pages/SalesSummaryPage';

const App = () => {
  return (
    <Router>
      <Navbar />
      <div style={{ padding: '20px' }}>
        <Routes>
          <Route path="/events" element={<EventListingPage />} />
          <Route path="/sales-summary" element={<SalesSummaryPage />} />
          {/* Redirect the default route (/) to /events */}
          <Route path="/" element={<Navigate to="/events" />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;