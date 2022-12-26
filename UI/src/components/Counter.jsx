import { React, useState } from "react";
import { Button, Container } from "react-bootstrap";

function Counter() {
  const [currentCount, setCurrentCount] = useState(0);

  function incrementCounter() {
    setCurrentCount(currentCount + 1);
  }

  function submitForm(e) {
    e.preventDefault();
    const file = document.getElementById("file");
    const number = document.getElementById("number");
    const id = number.value;
    const formData = new FormData();
    formData.append("Image", file.files[0]);
    // formData.append("Json", "{}");
    formData.append(
      "Json",
      JSON.stringify({
        Title: "Człowiek Nietoperz",
        AuthorId: 3,
        ReleaseYear: 1997,
        Description: "Pierwszy tom serii o Harrym Hole",
        Category: "Kryminał/Sensacja",
      })
    );

    //   "Json",
    //   JSON.stringify({
    //     Name: "Albert Rampampam",
    //     BirthDate: "1948-06-21T00:00:00",
    //     DeathDate: "1953-01-01",
    //     Status: 1,
    //   })
    // );

    console.log(formData);
    fetch(`/api/books`, {
      method: "POST",
      body: formData,
    })
      .then((res) => console.log(res))
      .catch((err) => ("Error occured", err));
  }

  return (
    <Container>
      <h1>Counter</h1>

      <div className="container">
        <h1>File Upload</h1>
        <form id="form">
          <div className="input-group">
            <label htmlFor="file">Select file</label>
            <input id="file" type="file" />
            <input id="number" type="number" />
          </div>
          <button className="submit-btn" type="submit" onClick={submitForm}>
            Upload
          </button>
        </form>
      </div>

      <p>This is a simple example of a React component.</p>

      <p aria-live="polite">
        Current count: <strong>{currentCount}</strong>
      </p>

      <Button variant="primary" onClick={incrementCounter}>
        Increment
      </Button>
    </Container>
  );
}

export default Counter;
