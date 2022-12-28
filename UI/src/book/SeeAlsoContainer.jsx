import { React, useState } from "react";
import {
  Collapse,
  Container,
  OverlayTrigger,
  Row,
  Tooltip
} from "react-bootstrap";
import SeeAlsoBookCard from "./SeeAlsoBookCard";

function SeeAlsoContainer(props) {
  const [collapse, setCollapse] = useState(false);

  const booksView = props.books.map((book) => {
    return <SeeAlsoBookCard key={book.Id} book={book} />;
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
            See also from {props.author.Name}:
          </h4>
        </OverlayTrigger>
      </Row>
      <Collapse in={!collapse}>
        <Row>{booksView}</Row>
      </Collapse>
    </Container>
  );
}

export default SeeAlsoContainer;
