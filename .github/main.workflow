workflow "Main Workflow" {
  on = "push"
  resolves = ["npm"]
}

action "npm" {
  uses = "actions/npm@master"
  args = "install"
}