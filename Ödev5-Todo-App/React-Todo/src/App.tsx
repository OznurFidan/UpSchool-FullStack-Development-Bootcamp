import React, { useState } from "react";
import { Container, Header, Input, List, Icon,Button, Select } from "semantic-ui-react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt } from "@fortawesome/free-solid-svg-icons";
import "./App.css";

interface Todo {
  id: number;
  task: string;
  isCompleted: boolean;
  createDate: string;
}

const App: React.FC = () => {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [newTodo, setNewTodo] = useState("");
  const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");

  const addTodo = () => {
    if (newTodo.trim() === "") return;

    const todo: Todo = {
      id: Date.now(),
      task: newTodo,
      isCompleted: false,
      createDate: new Date().toLocaleString(),
    };

    setTodos((prevTodos) => [...prevTodos, todo]);
    setNewTodo("");
  };

  const toggleCompleted = (id: number) => {
    setTodos((prevTodos) =>
      prevTodos.map((todo) =>
        todo.id === id ? { ...todo, isCompleted: !todo.isCompleted } : todo
      )
    );
  };

  const deleteTodo = (id: number) => {
    setTodos((prevTodos) => prevTodos.filter((todo) => todo.id !== id));
  };

  const sortTodosByDate = (data: any) => {
    const value = data.value as string;
    const newSortOrder = value === "true" ? "asc" : "desc";
    setSortOrder(newSortOrder);
  };

  const sortedTodos = [...todos].sort((a, b) =>
    sortOrder === "asc"
      ? new Date(a.createDate).getTime() - new Date(b.createDate).getTime()
      : new Date(b.createDate).getTime() - new Date(a.createDate).getTime()
  );

  const sortOptions = [
    { key: "asc", text: "Ascending", value: "true" },
    { key: "desc", text: "Descending", value: "false" },
  ];






  return (
    <Container text>

      <div className="app-container">
        <h1>TODO APPLICATION</h1>
          <div className="add-todo">
            <form className="todo-form">
            <Input className="todo-input"
            value={newTodo}
            onChange={(e) => setNewTodo(e.target.value)}
            placeholder="Enter a new todo"
          />
          <Button
            icon="plus"
            content="Add"
            onClick={addTodo}
            disabled={newTodo.trim() === ""}
            className="add-button"
          />
            </form>
          
          
          </div>
        
       <Header as="h1">Todos</Header>
       <div className="todo-list">
        <List divided relaxed>
          {todos.map((todo) => (
            
            <List.Item
              key={todo.id}
              onDoubleClick={() => toggleCompleted(todo.id)}
              className={todo.isCompleted ? "completed" : ""}
            >
              <List.Content floated="right">
               
              </List.Content>
              <List.Content>
                <List.Header>
                  <span
                    className={todo.isCompleted ? "completed-task" : ""}
                  >
                    {todo.task}
                  </span>
                  <Button
                  icon
                  onClick={() => deleteTodo(todo.id)}
                  className="delete-button"
                >
                  <FontAwesomeIcon icon={faTrashAlt} />
                </Button>
                </List.Header>
                <List.Description>
                   {todo.createDate}
                </List.Description>
              </List.Content>
            </List.Item>
          ))}
        </List>
        </div>
        <div className="sort-section ui fitted segment d-flex justify-center" >
          <Button className="select">
         <Select className="ml-2" placeholder='Select Sort Option' options={sortOptions} onChange={(_, data) => sortTodosByDate(data )} />
         </Button>
        </div>
      </div>
    </Container>
  );
};



export default App;