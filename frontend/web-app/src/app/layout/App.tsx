import 'bootstrap/dist/css/bootstrap.min.css';
import NavBar from "./NavBar";
import { Outlet } from 'react-router';
function App() {


return  (
    <>
      <NavBar/>
      <Outlet/>
    </>
  )
  
}

export default App
