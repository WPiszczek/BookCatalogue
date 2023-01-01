import { React, useState } from "react";
import {
  Collapse,
  Container,
  OverlayTrigger,
  Row,
  Tooltip
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAngleUp, faAngleDown } from "@fortawesome/free-solid-svg-icons";
import OtherBooksBookCard from "./OtherBooksBookCard";

function OtherBooksContainer(props) {
  const [collapse, setCollapse] = useState(false);

  const booksView = props.books.map((book) => {
    return <OtherBooksBookCard key={book.Id} book={book} />;
  });

  return (
    <Container fluid="md">
      <Row md={2}>
        <OverlayTrigger
          placement="top"
          overlay={
            <Tooltip>
              {collapse ? "Click to uncollapse" : "Click to collapse"}
            </Tooltip>
          }>
          <h4
            className="pointer-on-hover container-header-string"
            onClick={() => setCollapse(!collapse)}>
            {props.headerString}{" "}
            <FontAwesomeIcon icon={collapse ? faAngleDown : faAngleUp} />
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
