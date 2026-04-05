import { BrowserRouter, Routes, Route } from "react-router-dom";
import ApplicationList from "./pages/ApplicationList";
import CreateApplication from "./pages/CreateApplication";
import UpdateApplication from "./pages/UpdateApplication";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<ApplicationList />} />
        <Route path="/create" element={<CreateApplication />} />
        <Route path="/edit/:id" element={<UpdateApplication />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;