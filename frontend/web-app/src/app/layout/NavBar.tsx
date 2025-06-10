import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from 'react-router';


function NavBar() {
  return (
    <>
      <Navbar bg="primary" data-bs-theme="dark">
        <Container>
          <Navbar.Brand as={Link} to=""><img
              src="/vite.svg"
              width="30"
              height="30"
              className="d-inline-block align-top"
              alt="React Bootstrap logo"
            />
            Video Games</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link  as={Link} to="" >Home</Nav.Link>
            <Nav.Link as={Link} to="/videogames">Games</Nav.Link>
            <Nav.Link as={Link} to="/addVideogame">Add</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
}

export default NavBar;