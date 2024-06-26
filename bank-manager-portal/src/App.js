import "./App.css";
import NavBar from "./components/navbar/navbar";
import Accounts from "./routes/accounts/accounts";
import Dashboard from "./routes/dashboard/dashbord";
import Investments from "./routes/investments/investments";
import Payments from "./routes/payments/payments";
import Settings from "./routes/settings/settings";
import Statements from "./routes/statements/statements";
import Transactions from "./routes/transactions/transactions";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <div className="App">
        <NavBar />
        <Routes>
          <Route path="/accounts" element={<Accounts />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/investments" element={<Investments/>} />
          <Route path="/payments" element={<Payments/>} />
          <Route path="/settings" element={<Settings/>} />
          <Route path="/statements" element={<Statements/>} />
          <Route path="/transactions" element={<Transactions/>} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
