import React from "react";
import { Container } from "react-bootstrap";
import NavMenu from "./NavMenu";

function Layout(props) {
  return (
    <div>
      <NavMenu />
      <Container>{props.children}</Container>
    </div>
  );
}

export default Layout;
