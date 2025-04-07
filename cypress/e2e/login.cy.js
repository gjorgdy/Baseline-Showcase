describe('Login', () => {
  beforeEach(() => {
    cy.visit('https://baseline.hexasis.eu/')
    // Accept cookies
    // cy.get('button').click()
  })

  it('Go to login page if not logged in', () => {
    
    cy.get('#navAccount').click()

    cy.contains('Login')
        .should('be.visible')
  })
})