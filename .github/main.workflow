workflow "Main Workflow" {
  on = "push"
  resolves = ["Hello World"]
}

action "Hello World" {
  uses = "echo"
  args = "\"Hello world\""
}