describe('Profiles', () => {
  beforeEach(() => {
    cy.visit('https://baseline.hexasis.eu/')
    // Accept cookies
    cy.get('button').click()
  })

  it('Go to developer\'s profile', () => {
    
    cy.get('.linkButton').click()

    cy.contains('Dyce Insing')
        .parent()
        .parent()
        .should('be.visible')
  })

  it('Go to profile with id 1', () => {

    cy.get('#profileId')
        .type('1')
    
    cy.get('#gotoProfile')
        .click()

    cy.get('.profile-picture')
        .should('be.visible')
    
  })

})