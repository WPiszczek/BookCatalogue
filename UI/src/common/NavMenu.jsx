import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import AppRoutes from "../utils/AppRoutes";

function NavMenu() {
  const expand = "md";

  const nav = AppRoutes.map((route, i) => {
    return (
      <Nav.Link key={i} href={route.path}>
        {route.name}
      </Nav.Link>
    );
  });

  return (
    <Navbar collapseOnSelect expand={expand} bg="dark" variant="dark">
      <Container fluid={expand}>
        <Navbar.Brand href="/">Book Catalogue</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="ms-auto">{nav}</Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavMenu;
