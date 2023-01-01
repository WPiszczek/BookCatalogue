import { React, useState, useEffect } from "react";
import {
  Row,
  Col,
  Form,
  FloatingLabel,
  Button,
  Dropdown
} from "react-bootstrap";
import { AuthorStatusMap } from "../utils/EnumMaps";
import axios from "axios";

function AuthorsFilters(props) {
  const [name, setName] = useState("");
  const [status, setStatus] = useState("");

  const statusSelect = Object.keys(AuthorStatusMap).map((key) => {
    return (
      <option key={key} value={key}>
        {AuthorStatusMap[key]}
      </option>
    );
  });

  const filterAuthors = (event) => {
    event.preventDefault();
    console.log("Filter authors", name, status);
    props.fetchAuthorsData(name, status);
  };

  const addAuthor = (event) => {
    event.preventDefault();
    console.log("Add author");
    props.addAuthor();
  };

  const sortAuthors = (event, column = "Name", ascending = true) => {
    event.preventDefault();
    console.log("Sort authors ", column, ascending);
    props.sortAuthors(column, ascending);
  };

  return (
    <Form>
      <Row>
        <Col md={6}>
          <Row md={1}>
            <FloatingLabel label="Name" className="floating-label">
              <Form.Control
                type="text"
                name="Name"
                onChange={(e) => setName(e.target.value)}
              />
            </FloatingLabel>
          </Row>
          <Row>
            <FloatingLabel label="Status" className="floating-label">
              <Form.Select
                name="Status"
                onChange={(e) => setStatus(e.target.value)}>
                <option value={null} default></option>
                {statusSelect}
              </Form.Select>
            </FloatingLabel>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Button
              variant="primary"
              className="filters-button"
              onClick={filterAuthors}>
              Filter authors
            </Button>
            <Button
              variant="secondary"
              className="filters-button"
              onClick={addAuthor}>
              Add new author
            </Button>
          </Row>
        </Col>
        <Col md={3}>
          <Row>
            <Dropdown>
              <Dropdown.Toggle variant="success" className="filters-button">
                Sort authors
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item onClick={(e) => sortAuthors(e, "Name", true)}>
                  Sort by name ascending
                </Dropdown.Item>
                <Dropdown.Item onClick={(e) => sortAuthors(e, "Name", false)}>
                  Sort by name descending
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={(e) => sortAuthors(e, "AverageRating", true)}>
                  Sort by rating ascending
                </Dropdown.Item>
                <Dropdown.Item
                  onClick={(e) => sortAuthors(e, "AverageRating", false)}>
                  Sort by rating descending
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Row>
        </Col>
      </Row>
    </Form>
  );
}

export default AuthorsFilters;
