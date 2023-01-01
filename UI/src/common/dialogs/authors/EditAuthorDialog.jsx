import { React, useState } from "react";
import {
  Button,
  Modal,
  Row,
  FloatingLabel,
  Form,
  Alert
} from "react-bootstrap";
import axios from "axios";
import { AuthorStatusMap } from "../../../utils/EnumMaps";

function EditAuthorDialog(props) {
  const id = props.author.Id;
  const [show, setShow] = useState(props.show);
  const [name, setName] = useState(props.author.Name);
  const [country, setCountry] = useState(props.author.Country);
  const [birthDate, setBirthDate] = useState(
    props.author.BirthDate.substring(0, 10)
  );
  const [deathDate, setDeathDate] = useState(
    props.author.DeathDate ? props.author.DeathDate.substring(0, 10) : ""
  );
  const [status, setStatus] = useState(props.author.Status);
  const [responseMessage, setResponseMessage] = useState();

  const handleClose = () => {
    setShow(false);
    props.close();
  };

  const editAuthor = async (event) => {
    event.preventDefault();
    const author = {
      Name: name,
      Country: country,
      BirthDate: birthDate,
      DeathDate: deathDate,
      Status: parseInt(status)
    };
    console.log("Author ", author);

    await axios
      .put(`/api/authors/${id}`, author)
      .then((response) => {
        console.log(response);
        if (response.data.Status === "Success") {
          console.log("Success");
          setResponseMessage(
            <Alert variant="success">{response.data.Message}</Alert>
          );
          props.fetchData();
        } else {
          console.log("Fail ", response.data.Message);
          setResponseMessage(
            <Alert variant="danger">{response.data.Message}</Alert>
          );
        }
      })
      .catch((error) => {
        console.log(error.message);
        setResponseMessage(
          <Alert variant="danger">Something went wrong. Try again.</Alert>
        );
      });
  };

  const statusSelect = Object.keys(AuthorStatusMap).map((key) => {
    return (
      <option key={key} value={key}>
        {AuthorStatusMap[key]}
      </option>
    );
  });

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Edit {props.author.Name}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row md={1}>{responseMessage ?? <></>}</Row>
        <Row>
          <FloatingLabel label="Name" className="floating-label">
            <Form.Control
              required
              type="text"
              placeholder="Enter author name"
              value={name}
              name="Name"
              onChange={(e) => setName(e.target.value)}
            />
          </FloatingLabel>
        </Row>
        <Row md={2}>
          <FloatingLabel label="Country" className="floating-label">
            <Form.Control
              required
              type="text"
              placeholder="Enter author country"
              value={country}
              name="Country"
              onChange={(e) => setCountry(e.target.value)}
            />
          </FloatingLabel>
          <FloatingLabel label="Status" className="floating-label">
            <Form.Select
              required
              name="Status"
              value={status}
              onChange={(e) => {
                setStatus(e.target.value);
                if (AuthorStatusMap[e.target.value] !== "Dead") {
                  setDeathDate("");
                }
              }}>
              <option value={null} default></option>
              {statusSelect}
            </Form.Select>
          </FloatingLabel>
        </Row>
        <Row md={2}>
          <FloatingLabel label="Birth Date" className="floating-label">
            <Form.Control
              required
              type="date"
              name="BirthDate"
              placeholder="Enter birth date"
              value={birthDate}
              onChange={(e) => setBirthDate(e.target.value)}
            />
          </FloatingLabel>
          <FloatingLabel label="Death Date" className="floating-label">
            <Form.Control
              disabled={AuthorStatusMap[status] !== "Dead" ? "disabled" : ""}
              type="date"
              name="DeathDate"
              placeholder="Enter death date"
              value={AuthorStatusMap[status] !== "Dead" ? "" : deathDate}
              onChange={(e) => setDeathDate(e.target.value)}
            />
          </FloatingLabel>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="primary" onClick={editAuthor}>
          Confirm
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default EditAuthorDialog;
