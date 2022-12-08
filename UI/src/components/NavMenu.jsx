import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import AppRoutes from '../AppRoutes';

function NavMenu() {
  const expand = 'md';

  const nav = AppRoutes.map((route) => {
    const { element, ...rest } = route;
    return <Nav.Link href={route.path}>{route.name}</Nav.Link>;
  });

  return (
    <Navbar collapseOnSelect expand={expand} bg='dark' variant='dark'>
      <Container fluid={expand}>
        <Navbar.Brand href='/'>Book Catalogue</Navbar.Brand>
        <Navbar.Toggle aria-controls='responsive-navbar-nav' />
        <Navbar.Collapse id='responsive-navbar-nav'>
          <Nav className='ms-auto'>{nav}</Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavMenu;