import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

import Navbar from './components/Navbar';
import EventListingPage from './pages/EventListingPage';
import SalesSummaryPage from './pages/SalesSummaryPage';

const App = () => {
  return (
    <Router>
      <Navbar />
      <div className="p-3">
        <Routes>
          <Route path="/events" element={<EventListingPage />} />
          <Route path="/sales-summary" element={<SalesSummaryPage />} />
          <Route path="/" element={<Navigate to="/events" />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;