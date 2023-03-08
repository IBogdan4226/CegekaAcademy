import './App.css';
import { Pets } from './Pages/Pets';
import { Route, Routes } from 'react-router-dom';
import { Home } from './Pages/Home';
import { NoMatch } from './Pages/NoMatch';
import { Layout } from './HOC/Layout';
import { Fundraisers } from './Pages/Fundraisers';
import AddPet from './Pages/AddPet';
function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/Pets" element={<Pets></Pets>} />
        <Route path="/Pets/Rescue" element={<AddPet></AddPet>} />
        <Route path="/Fundraisers" element={<Fundraisers/>} />
        <Route path="/" element={<Home></Home>} />
        <Route path="*" element={<NoMatch></NoMatch>} />
      </Routes>
    </Layout>
  );
}

export default App;
