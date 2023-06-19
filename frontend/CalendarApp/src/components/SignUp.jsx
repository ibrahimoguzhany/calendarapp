import React from "react";
import axios from "axios";
import { useState } from "react";
import { API_ROUTES, APP_ROUTES } from "../utils/constants";
import { Link, useNavigate } from "react-router-dom";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  Container,
  Row,
  Col,
  Alert,
  FormFeedback,
} from "reactstrap";
import { Spinner } from "reactstrap";

const SignUp = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState({});
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    username: "",
    identityNumber: "",
    password: "",
    passwordConfirm: "",
    phone: "",
    email: "",
    address: "",
  });

  const setField = (field, value) => {
    setForm({
      ...form,
      [field]: value,
    });
    // clear errors
    if (!!errors[field])
      setErrors({
        ...errors,
        [field]: null,
      });
  };

  const findFormErrors = () => {
    const { username, password, passwordConfirm, email } = form;
    const newErrors = {};
    // Username errors
    if (!username || username === "")
      newErrors.username = "Kullanıcı adı gereklidir!";
    // Password errors
    if (!password || password === "") newErrors.password = "Parola gereklidir!";
    // Password confirm errors
    if (password !== passwordConfirm)
      newErrors.passwordConfirm = "Parolalar eşleşmiyor!";
    // Email errors
    if (!email || email === "") newErrors.email = "E-posta gereklidir!";

    return newErrors;
  };

  const signUp = async () => {
    setIsLoading(true);
    // find errors
    const newErrors = findFormErrors();
    if (Object.keys(newErrors).length > 0) {
      // We got errors!
      setErrors(newErrors);
      setIsLoading(false);
      return;
    } else {
      // No errors, submit form!
      setIsLoading(true);
      try {
        const response = await axios({
          method: "POST",
          url: API_ROUTES.REGISTER,
          data: form,
        });
        if (!response?.data?.isSuccess) {
          console.log("Kayıt olurken bir sorun oluştu: ", response);
          return;
        }
        navigate(APP_ROUTES.SIGN_IN);
      } catch (err) {
        console.log("Kayıt olurken bir hata oluştu: ", err);
      } finally {
        setIsLoading(false);
      }
    }
  };

  return (
    <Container className="vh-100 d-flex justify-content-center align-items-center">
      <Col sm="12" md="9" lg="6" className="mx-auto">
        <div className="shadow-lg rounded bg-white p-4">
          <h2 className="text-center font-weight-medium mb-4">Kayıt Ol</h2>
          <Form>
            <Row form>
              <Col md={6}>
                <FormGroup>
                  <Label for="firstName" className="sr-only">
                    Ad
                  </Label>
                  <Input
                    type="text"
                    name="firstName"
                    id="firstName"
                    placeholder="Ad"
                    onChange={(e) => setField("firstName", e.target.value)}
                    className="border rounded p-2"
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="lastName" className="sr-only">
                    Soyad
                  </Label>
                  <Input
                    type="text"
                    name="lastName"
                    id="lastName"
                    placeholder="Soyad"
                    onChange={(e) => setField("lastName", e.target.value)}
                    className="border rounded p-2"
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="username" className="sr-only">
                    Kullanıcı Adı
                  </Label>
                  <Input
                    type="text"
                    name="username"
                    id="username"
                    placeholder="Kullanıcı Adı"
                    onChange={(e) => setField("username", e.target.value)}
                    className="border rounded p-2"
                    invalid={!!errors.username}
                  />
                  <FormFeedback>{errors.username}</FormFeedback>
                </FormGroup>
                <FormGroup>
                  <Label for="identityNumber" className="sr-only">
                    TC Kimlik No
                  </Label>
                  <Input
                    type="text"
                    name="identityNumber"
                    id="identityNumber"
                    placeholder="TC Kimlik No"
                    onChange={(e) => setField("identityNumber", e.target.value)}
                    className="border rounded p-2"
                  />
                </FormGroup>
              </Col>
              <Col md={6}>
                <FormGroup>
                  <Label for="password" className="sr-only">
                    Parola
                  </Label>
                  <Input
                    type="password"
                    name="password"
                    id="password"
                    placeholder="Parola"
                    onChange={(e) => setField("password", e.target.value)}
                    className="border rounded p-2"
                    invalid={!!errors.password}
                  />
                  <FormFeedback>{errors.password}</FormFeedback>
                </FormGroup>
                <FormGroup>
                  <Label for="passwordConfirm" className="sr-only">
                    Parola Onayı
                  </Label>
                  <Input
                    type="password"
                    name="passwordConfirm"
                    id="passwordConfirm"
                    placeholder="Parolayı Onayla"
                    onChange={(e) =>
                      setField("passwordConfirm", e.target.value)
                    }
                    className="border rounded p-2"
                    invalid={!!errors.passwordConfirm}
                  />
                  <FormFeedback>{errors.passwordConfirm}</FormFeedback>
                </FormGroup>
                <FormGroup>
                  <Label for="phone" className="sr-only">
                    Telefon
                  </Label>
                  <Input
                    type="text"
                    name="phone"
                    id="phone"
                    placeholder="Telefon"
                    onChange={(e) => setField("phone", e.target.value)}
                    className="border rounded p-2"
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="email" className="sr-only">
                    E-posta
                  </Label>
                  <Input
                    type="email"
                    name="email"
                    id="email"
                    placeholder="E-posta"
                    onChange={(e) => setField("email", e.target.value)}
                    className="border rounded p-2"
                    invalid={!!errors.email}
                  />
                  <FormFeedback>{errors.email}</FormFeedback>
                </FormGroup>
                <FormGroup>
                  <Label for="address" className="sr-only">
                    Adres
                  </Label>
                  <Input
                    type="textarea"
                    name="address"
                    id="address"
                    placeholder="Adres"
                    onChange={(e) => setField("address", e.target.value)}
                    className="border rounded p-2"
                    rows={3}
                  />
                </FormGroup>
              </Col>
            </Row>
            <Button
              onClick={signUp}
              className="d-flex justify-content-center align-items-center mt-4 mx-auto"
              style={{ width: "50%" }}
            >
              {isLoading ? (
                <Spinner size="sm" color="dark" className="mr-2" />
              ) : null}
              <span>Kayıt Ol</span>
            </Button>
          </Form>
          <div className="text-center small mt-3">
            Zaten üye misiniz?
            <Link to="/signin">
              <span className="font-weight-medium text-dark ml-1">
                Giriş Yap
              </span>
            </Link>
          </div>
        </div>
      </Col>
    </Container>
  );
};

export default SignUp;
