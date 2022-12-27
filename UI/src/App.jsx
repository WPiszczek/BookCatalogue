import React from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import AppRoutes from "./utils/AppRoutes";
import Layout from "./common/Layout";

function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<Navigate to="/books" />} />
        {AppRoutes.map((route, index) => {
          const { element, ...rest } = route;
          return <Route key={index} {...rest} element={element} />;
        })}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Layout>
  );
}

export default App;
