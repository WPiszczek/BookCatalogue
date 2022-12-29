import { React, useState } from "react";
import {
  Collapse,
  Container,
  OverlayTrigger,
  Row,
  Tooltip
} from "react-bootstrap";
import OtherBooksBookCard from "./OtherBooksBookCard";

function OtherBooksContainer(props) {
  const [collapse, setCollapse] = useState(false);

  const booksView = props.books.map((book) => {
    return <OtherBooksBookCard key={book.Id} book={book} />;
  });

  return (
    <Container fluid="md">
      <Row md={3}>
        <OverlayTrigger
          placement="top"
          overlay={
            <Tooltip>
              {collapse ? "Click to uncollapse" : "Click to collapse"}
            </Tooltip>
          }>
          <h4
            className="pointer-on-hover"
            onClick={() => setCollapse(!collapse)}>
            {props.headerString}
          </h4>
        </OverlayTrigger>
      </Row>
      <Collapse in={!collapse}>
        <Row>{booksView}</Row>
      </Collapse>
    </Container>
  );
}

export default OtherBooksContainer;
